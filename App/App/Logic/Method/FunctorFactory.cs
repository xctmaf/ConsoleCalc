using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Logic.Method.Abstract;
using System.Collections;

namespace App.Logic.Method
{
    public class FunctorFactory : IEnumerable<Functor>
    {
        private ICollection<Functor> _functors;

        public FunctorFactory() : this(new Collection<Functor>()) { }
        public FunctorFactory(ICollection<Functor> functors)
        {
            _functors = functors;
        }

        public FunctorFactory Add(Functor newFunctor)
        {
            if (newFunctor == null)
                throw new ArgumentException("Ошибка определения доступных операций");
            
            var func = GetFunctor(newFunctor.Record);
            if (func != null)
                _functors.Remove(func);
            _functors.Add(newFunctor);

            return this;
        }

        public FunctorFactory Clear()
        {
            _functors.Clear();
            return this;
        }

        public int Count()
        {
            return _functors.Count;
        }

        public Functor GetFunctor(string record)
        {
            foreach (Functor func in _functors)
            {
                if (func.Record == record)
                    return func;
            }
            return null;
        }

        public IEnumerator<Functor> GetEnumerator()
        {
            return _functors.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _functors.GetEnumerator();
        }
    }
}
