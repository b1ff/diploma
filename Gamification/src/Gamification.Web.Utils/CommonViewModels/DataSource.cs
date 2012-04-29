using System;
using System.Collections.Generic;
using System.Linq;

namespace Gamification.Web.Utils.CommonViewModels
{
    [Serializable]
    public class DataSource : SortedSet<NumericSelectListItem> // put string here probably
    {
        public DataSource(int id)
        {
            Id = id;
        }

        public void Set(NumericSelectListItem viewModel)
        {
            Id = viewModel.Value;
            AddOrUpdate(viewModel);
        }

        public int Id { get; set; }

        public string CurrentValue
        {
            get
            {
                var valueItem = this.FirstOrDefault(x => x.Value == Id);
                return valueItem != null ? valueItem.Text ?? valueItem.Value.ToString() : string.Empty;
            }
        }

        // dngerous method due to AutoMapper usage, some mappings
        // that expect this behaviour may 
        public static implicit operator int(DataSource dataSource)
        {
            return dataSource.Id;
        }

        // Current method is used for proper selected value calculation
        // for DropDownFor and ralted methods
        // due to their internal implementation.
        public override string ToString()
        {
            return Id.ToString();
        }

        private bool ContainsId(int id)
        {
            return this.Any(x => x.Value == id);
        }

        public void AddOrUpdate(NumericSelectListItem item)
        {
            if (ContainsId(item.Value))
            {
                this.RemoveWhere(x => x.Value == item.Value);
            }
            else
            {
                Add(item);
            }
        }
    }
}
