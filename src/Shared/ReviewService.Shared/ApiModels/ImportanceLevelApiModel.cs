using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ReviewService.Shared.ApiModels
{
    public class ImportanceLevelApiModel: IEquatable<ImportanceLevelApiModel>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Color { get; set; }

        public bool Equals(ImportanceLevelApiModel other)
        {
            if (other == null)
                return false;

            if (this.GetHashCode() == other.GetHashCode())
                return true;
            else
                return false;
        }

        public override bool Equals(Object obj)
        {
            if (obj == null)
                return false;

            ImportanceLevelApiModel levelObj = obj as ImportanceLevelApiModel;
            if (levelObj == null)
                return false;
            else
                return Equals(levelObj);
        }

        public override int GetHashCode()
        {
            return this.Name.GetHashCode() + this.Description.GetHashCode() + this.Color.GetHashCode();
        }
    }
}
