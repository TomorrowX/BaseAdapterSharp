using System;
namespace BaseAdapterSharp
{
	public class Jbox<T> : Java.Lang.Object
	{
		T obj;
		public Jbox(T obj)
		{
			this.obj = obj;
		}
		public T Value
		{
			get
			{
				return obj;
			}
		}
	}
}
