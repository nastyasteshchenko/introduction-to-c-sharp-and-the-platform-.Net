using Common.Message;
using MassTransit;

namespace HrDirector;

public class HackathonEventManager(
    Options options,
    IPublishEndpoint publishEndpoint,
    IHostApplicationLifetime lifetime)
{
    private int _currentHackathonEventTime = 1;

    public bool IsNeedNextHackathon()
    {
        return options.HackathonEventTimes != _currentHackathonEventTime;
    }

    public void StartNewHackathon()
    {
        _currentHackathonEventTime++;
        publishEndpoint.Publish(new HackathonStarted
        {
            HackathonId = _currentHackathonEventTime
        });
    }

    public void StopHackathons()
    {
        publishEndpoint.Publish(new HackathonsStopped());
        lifetime.StopApplication();
    }
}