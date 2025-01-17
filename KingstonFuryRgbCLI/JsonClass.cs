using System;
using System.Collections.Generic;
using System.Reflection;
using System.Web.Script.Serialization;

namespace KingstonFuryRgbCLI
{
	// Token: 0x0200001D RID: 29
	public class JsonClass
	{
		// Token: 0x06000138 RID: 312 RVA: 0x000082E8 File Offset: 0x000064E8
		public static T JavaScriptDeserialize<T>(string jsonString)
		{
			return new JavaScriptSerializer().Deserialize<T>(jsonString);
		}

		// Token: 0x06000139 RID: 313 RVA: 0x000082F8 File Offset: 0x000064F8
		public static string JavaScriptSerialize<T>(T t)
		{
			JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
			javaScriptSerializer.RegisterConverters(new JavaScriptConverter[]
			{
				new JsonClass.NullPropertiesConverter()
			});
			return javaScriptSerializer.Serialize(t);
		}

		// Token: 0x02000048 RID: 72
		public class NullPropertiesConverter : JavaScriptConverter
		{
			// Token: 0x060001ED RID: 493 RVA: 0x00014ECB File Offset: 0x000130CB
			public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060001EE RID: 494 RVA: 0x00014ED4 File Offset: 0x000130D4
			public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
			{
				Dictionary<string, object> dictionary = new Dictionary<string, object>();
				foreach (PropertyInfo propertyInfo in obj.GetType().GetProperties())
				{
					bool flag = propertyInfo.IsDefined(typeof(ScriptIgnoreAttribute), true);
					object value = propertyInfo.GetValue(obj, BindingFlags.Public, null, null, null);
					if (value != null && !flag)
					{
						dictionary.Add(propertyInfo.Name, value);
					}
				}
				return dictionary;
			}

			// Token: 0x17000066 RID: 102
			// (get) Token: 0x060001EF RID: 495 RVA: 0x00014F3D File Offset: 0x0001313D
			public override IEnumerable<Type> SupportedTypes
			{
				get
				{
					return base.GetType().Assembly.GetTypes();
				}
			}
		}
	}
}
