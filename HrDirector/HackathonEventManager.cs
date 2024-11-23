using Common.Message;
using MassTransit;

namespace HrDirector;

public class HackathonEventManager(
    Options options,
    IPublishEndpoint publishEndpoint,
    IHostApplicationLifetime lifetime)
{
    private int CurrentHackathonEventTime = 0;

    public Boolean IsNeedNextHackathon()
    {
        return options.HackathonEventTimes == CurrentHackathonEventTime;
    }

    public void StartNewHackathon()
    {
        CurrentHackathonEventTime++;
        publishEndpoint.Publish(new StartHackathonMessage()
        {
            HackathonId = CurrentHackathonEventTime
        });
    }

    public void StopHackathons()
    {
        publishEndpoint.Publish(new StopHackathonsMessage());
        lifetime.StopApplication();
    }
}