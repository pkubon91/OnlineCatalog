﻿using System;
using Business.Groups;
using Business.NHibernate;
using NHibernate;
using OnlineCatalog.Common.Extensions;

namespace Business.DataAccess.Group
{
    public class ShopRepository : IShopRepository
    {
        private readonly ISessionProvider _sessionProvider;

        public ShopRepository(ISessionProvider sessionProvider)
        {
            if (sessionProvider == null) throw new ArgumentNullException(nameof(sessionProvider));
            _sessionProvider = sessionProvider;
        }

        public void AddToDatabase(Shop entity)
        {
            if(entity == null) throw new ArgumentNullException(nameof(entity));
            if(entity.Name.IsNullOrEmpty()) throw new ArgumentNullException(nameof(entity.Name));

            using (var session = _sessionProvider.CreateSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    session.Save(entity);
                    transaction.Commit();
                }
            }
        }

        public Shop GetShopByName(string shopName)
        {
            if(shopName.IsNullOrEmpty()) throw new ArgumentNullException(nameof(shopName));

            string queryString = @"from Shop s where s.Name = :Name";
            using (var session = _sessionProvider.CreateSession())
            {
                IQuery query = session.CreateQuery(queryString);
                query.SetParameter("Name", shopName);
                return query.UniqueResult<Shop>();
            }
        }

        public Shop GetShopById(Guid uniqueId)
        {
            using (var session = _sessionProvider.CreateSession())
            {
                return session.Get<Shop>(uniqueId);
            }
        }
    }
}