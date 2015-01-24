using System;
using System.Linq;

namespace RedditBet.API
{
    using AutoMapper;

    public interface IMappable<OutT> 
        where OutT : class
    {
        OutT ToMappedType();
    }

    public abstract class Mappable<T, OutT> : IMappable<OutT>
        where T : Mappable<T, OutT>
        where OutT : class
    {
        private Mappable<T, OutT> _derivedInstance 
        {
            get { return (T)this; }
        }

        /// <summary>
        /// When a derived class inherits from Mappable, this method allows auto-mapping to its "sibling" class, 
        /// which is usually ViewModel -> DomainModel or DomainModel -> ViewModel
        /// </summary>
        /// <returns></returns>
        public OutT ToMappedType()
        {
            try
            {
                Mapper.CreateMap<T, OutT>();
                return Mapper.Map<OutT>(_derivedInstance);
            }
            catch (Exception ex)
            { 
                // Todo: throw exception
                return null;
            }
        }
    }
}