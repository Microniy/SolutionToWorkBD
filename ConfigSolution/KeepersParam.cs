using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigSolution
{
    public enum Keepers
    // List of types of keepers for save you parameters
    {
        Reg
    };
    public abstract class Keeper
        // Abstract class for access and works in keepers classes
    {
        public static  Keeper Instance(Keepers TypeKeeper) 
        {
            switch (TypeKeeper)
            {
                //every Kepers to be havent branch this switch
                case Keepers.Reg:
                    return new RegKeeper();
                default:
                    return null;
            };
        }
        public abstract bool Save(string key, string value);
        public abstract string GetParam(string key);
    }
    internal class RegKeeper : Keeper
    {
        public override string GetParam(string key)
        {
            throw new NotImplementedException();
        }

        public override bool Save(string key, string value)
        {
            throw new NotImplementedException();
        }
    }
}
