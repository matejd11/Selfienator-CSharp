using System;

namespace Arduino.Selfienator.Core
{
    public interface IEventAggregator
    {
        void PublishEvent<TEventType>(TEventType eventToPublish);

        void SubsribeEvent(Object subscriber);
    }
}
