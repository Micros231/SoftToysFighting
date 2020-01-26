using System;
using System.Collections.Generic;
using System.Linq;
using Plugins.xNodeUtilityAi.Framework;
using XNode;
using Object = UnityEngine.Object;

namespace Plugins.xNodeUtilityAi.AbstractNodes {
    public abstract class CollectionEntryNode : EntryNode {
        
        [Input(ShowBackingValue.Never)] public TaggedData Data;
        [Output] public CollectionEntryNode LinkedOption;
        [Output] public TaggedData DataOut;
        public string DataTag = "Data";
        public int Index;

        public int CollectionCount => CollectionProvider(_context)?.Count ?? 0;

        protected abstract List<Object> CollectionProvider(AbstractAIComponent context);
        
        public override object GetValue(NodePort port) {
            if (port.fieldName == "LinkedOption") return this;
            if (port.fieldName == "DataOut") {
                if (_context == null) return null;
                List<Object> collection = CollectionProvider(_context);
                if (collection != null && collection.Count > Index) {
                    TaggedData taggedData = new TaggedData {
                        Data = CollectionProvider(_context)[Index], 
                        DataTag = DataTag
                    };
                    return taggedData;
                }
            }
            return null;
        }
        
        protected T GetData<T>() where T : Object {
            List<TaggedData> taggedDatas = GetInputValues<TaggedData>("Data").ToList();
            taggedDatas.RemoveAll(data => data == null);
            taggedDatas = taggedDatas.Where(data => data.Data is T).ToList();
            if (taggedDatas.Count > 1)
                throw new Exception("Multiple Data found for type " + typeof(T) + " in " + name + 
                                    ", you should consider using GetData with a dataTag as parameter");
            if (taggedDatas.Count > 0)
                return taggedDatas.First().Data as T;
            return null;
        }

        protected T GetData<T>(string dataTag) where T : Object {
            List<TaggedData> taggedDatas = GetInputValues<TaggedData>("Data").ToList();
            taggedDatas.RemoveAll(data => data == null);
            taggedDatas = taggedDatas.Where(data => data.Data is T && data.DataTag == dataTag).ToList();
            if (taggedDatas.Count > 1)
                throw new Exception("Multi Data found for type " + typeof(T) + " and tag " + dataTag + 
                                    " in " + name + ", don't use the same dataTag twice as input");
            if (taggedDatas.Count > 0)
                return taggedDatas.First().Data as T;
            return null;
        }
        
    }

}