using System;

namespace DIBasics_Autofac.Tests.M2.DL {
    internal interface IRepository {
        BondData GetBondData();
    }

    internal class BondData { }

    internal class SqlRepository : IRepository {
        public BondData GetBondData() {
            throw new NotImplementedException();
        }
    }

// ПЛОХО! Класс облигации использует конкретный класс SqlRepository
    internal class Bond1 {
        readonly SqlRepository _repository = new SqlRepository();

        public Bond1() {
            var bondData = _repository.GetBondData();
        }
    }

// ПЛОХО! Класс облигации использует "абстракцию" IRepository
    internal class Bond2 {
        readonly IRepository _repository;

        public Bond2(IRepository repository) {
            _repository = repository;
            var bondData = _repository.GetBondData();
        }
    }

// ХОРОШО! Класс облигации ничего не знает о слое доступа к данным!
    internal class Bond3 {
        public Bond3(BondData bondData) { }
    }
}
