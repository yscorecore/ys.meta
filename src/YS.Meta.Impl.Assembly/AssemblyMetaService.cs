using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using YS.Knife;

namespace YS.Meta.Impl.Assembly
{
    [Service]
    public class AssemblyMetaService : IMetaService
    {
        public Task<List<string>> GetAllKeys()
        {
            var allKeys = AppDomain.CurrentDomain.FindInstanceTypesByAttribute<MetaModelAttribute>()
                       .Select(p => MetaModelAttribute.GetMetaName(p))
                       .ToList();
            return Task.FromResult(allKeys);
        }
        public Task<MetaInfo> GetMeta(string name)
        {
            var metaInfo = AppDomain.CurrentDomain.FindInstanceTypesByAttribute<MetaModelAttribute>()
                        .Where(p => MetaModelAttribute.GetMetaName(p) == name)
                        .Select(p => CreateMetaInfo(p, name))
                        .FirstOrDefault();
            return Task.FromResult(metaInfo);

        }
        private MetaInfo CreateMetaInfo(Type modelType, string name)
        {

            AttributeCollection attributeCollection = TypeDescriptor.GetAttributes(modelType);
            var displayAttr = attributeCollection.OfType<DisplayAttribute>().FirstOrDefault();
            return new MetaInfo
            {
                Name = name,
                // DisplayName = GetDisplayName(displayAttr, attributeCollection),
                //Description = GetDescription(displayAttr, attributeCollection),
                Properties = GetProperties(modelType),
            };
        }
        private string GetDisplayName(DisplayAttribute displayAttr, PropertyDescriptor propertyInfo)
        {
            return displayAttr?.Name ?? propertyInfo.DisplayName;
        }
        private string GetDescription(DisplayAttribute displayAttr, PropertyDescriptor propertyInfo)
        {
            return displayAttr?.Description ?? propertyInfo.Description;
        }
        private string GetShortDisplayName(DisplayAttribute displayAttr, PropertyDescriptor propertyInfo)
        {
            return displayAttr?.ShortName ?? displayAttr?.Name ?? propertyInfo.DisplayName;
        }
        private bool IsKeyField(AttributeCollection attributeCollection, PropertyDescriptor propertyInfo)
        {
            return string.Equals("id", propertyInfo.Name, StringComparison.InvariantCultureIgnoreCase)
                || attributeCollection.OfType<KeyAttribute>().Any();
        }
        private string GetDataUnit(AttributeCollection attributeCollection)
        {
            return attributeCollection.OfType<DataUnitAttribute>().Select(p => p.Unit).FirstOrDefault();
        }
        private List<PropInfo> GetProperties(Type modelType)
        {
            return TypeDescriptor.GetProperties(modelType)
                 .Cast<PropertyDescriptor>()
                 .Select(GetPropertyInfo)
                 .ToList();
            //return modelType.GetProperties()
            //     .Select(GetPropertyInfo)
            //     .ToList();
        }
        private PropInfo GetPropertyInfo(PropertyDescriptor propertyInfo)
        {
            AttributeCollection attributeCollection = propertyInfo.Attributes;
            var displayAttr = attributeCollection.OfType<DisplayAttribute>().FirstOrDefault();
            var propType = propertyInfo.PropertyType;
            return new PropInfo
            {
                ReadOnly = propertyInfo.IsReadOnly,
                Name = propertyInfo.Name,
                DisplayName = GetDisplayName(displayAttr, propertyInfo),
                FieldTypeCode = Type.GetTypeCode(propType).ToString(),
                Description = GetDescription(displayAttr, propertyInfo),
                ShortDisplayName = GetShortDisplayName(displayAttr, propertyInfo),
                IsKey = IsKeyField(attributeCollection, propertyInfo),
                DataUnit = GetDataUnit(attributeCollection),
            };
        }


    }
}
