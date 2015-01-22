using System;
using System.Linq;

namespace RedditBet.API
{
    using AutoMapper;

    public interface IMappable<OutT> 
        where OutT : class
    {
        OutT ToDomainModel();
    }

    public abstract class Mappable<T, OutT> : IMappable<OutT>
        where T : Mappable<T, OutT>
        where OutT : class
    {
        private Mappable<T, OutT> _derivedInstance 
        {
            get { return (T)this; }
        }

        public OutT ToDomainModel()
        {
            try
            {
                Mapper.CreateMap<T, OutT>();
                return Mapper.Map<OutT>(_derivedInstance);
            }
            catch (Exception ex)
            { 
                // Todo throw exception
                return null;
            }
        }
    }
}