using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Gamification.Core.Entities
{
    [Serializable]
    public class Gamer : BaseEntity
    {
        public Gamer()
        {
            this.Achievements = new List<Achievement>();
            this.GamerStatuses = new HashSet<GamerStatus>();
        }

        public Project Project { get; set; }

        public long Points { get; set;  }

        public ISet<GamerStatus> GamerStatuses { get; set; }

        public Level CurrentLevel { get; set; }

        public IList<Achievement> Achievements { get; set; }

        public string UniqueKey { get; set; }

        public Gamer Clone()
        {
            using (var memStream = new MemoryStream())
            {
                var binaryFormatter = new BinaryFormatter(null,
                     new StreamingContext(StreamingContextStates.Clone));
                binaryFormatter.Serialize(memStream, this);
                memStream.Seek(0, SeekOrigin.Begin);
                return (Gamer)binaryFormatter.Deserialize(memStream);
            }
        }
    }
}