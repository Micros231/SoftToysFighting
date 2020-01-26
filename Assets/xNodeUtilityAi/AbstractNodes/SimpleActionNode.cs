using System.Collections.Generic;
using System.Linq;
using Plugins.xNodeUtilityAi.Framework;
using XNode;

namespace Plugins.xNodeUtilityAi.AbstractNodes {
    public abstract class SimpleActionNode : ActionNode {

        [Input(ShowBackingValue.Never)] public TaggedData Data;
        
        public List<TaggedData> GetData() {
            if (GetInputPort("Data") != null) {
                List<TaggedData> taggedDatas = GetInputValues<TaggedData>("Data").ToList();
                return taggedDatas;
            }
            return null;
        }
        
        public override object GetValue(NodePort port) {
            if (port.fieldName == "LinkedOption")
                return this;
            return null;
        }
        
    }

}