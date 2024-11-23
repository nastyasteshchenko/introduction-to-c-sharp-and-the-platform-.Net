using Common.Message;
using MassTransit;

namespace HrDirector;

public class HackathonEventManager(
    Options options,
    IPublishEndpoint publishEndpoint,
    IHostApplicationLifetime lifetime)
{
    private int _currentHackathonEventTime;

    public bool IsNeedNextHackathon()
    {
        return options.HackathonEventTimes != _currentHackathonEventTime;
    }

    public async void StartNewHackathon()
    {
        _currentHackathonEventTime++;
        await publishEndpoint.Publish(new HackathonStarted
        {
            HackathonId = _currentHackathonEventTime
        });
    }

    public async void StopHackathons()
    {
        await publishEndpoint.Publish(new HackathonsStopped());
        lifetime.StopApplication();
    }
}