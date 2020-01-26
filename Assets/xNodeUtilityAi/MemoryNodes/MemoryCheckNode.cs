using Plugins.xNodeUtilityAi.AbstractNodes;
using Plugins.xNodeUtilityAi.Framework;
using UnityEngine;

namespace Plugins.xNodeUtilityAi.MemoryNodes {
    public class MemoryCheckNode : SimpleEntryNode {

        protected override int ValueProvider(AbstractAIComponent context) {
            return GetData<Object>() != null ? 1 : 0;
        }
        
    }
}