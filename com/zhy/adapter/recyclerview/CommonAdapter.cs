

namespace Com.Zhy.Adapter.Recyclerview
{
	/// <summary>Created by zhy on 16/4/9.</summary>
	public abstract class CommonAdapter<T> : MultiItemTypeAdapter<T>
	{
		//protected internal Android.Content.Context mContext;

		protected internal int mLayoutId;

		//protected internal System.Collections.Generic.IList<T> mDatas;

		protected internal Android.Views.LayoutInflater mInflater;

		public CommonAdapter(Android.Content.Context context, int layoutId, System.Collections.Generic.IList<T> datas) : base(context, datas)
		{
			mContext = context;
			mInflater = Android.Views.LayoutInflater.From(context);
			mLayoutId = layoutId;
			mDatas = datas;
			AddItemViewDelegate(new _ItemViewDelegate_31(this, layoutId));
		}

		private sealed class _ItemViewDelegate_31 : Com.Zhy.Adapter.Recyclerview.Base.ItemViewDelegate<T>
		{
			public _ItemViewDelegate_31(CommonAdapter<T> _enclosing, int layoutId)
			{
				this._enclosing = _enclosing;
				this.layoutId = layoutId;
			}

			public int GetItemViewLayoutId()
			{
				return layoutId;
			}

			public bool IsForViewType(T item, int position)
			{
				return true;
			}

			public void Convert(Com.Zhy.Adapter.Recyclerview.Base.ViewHolder holder, T t, int
				 position)
			{
				this._enclosing.Convert(holder, t, position);
			}

			private readonly CommonAdapter<T> _enclosing;

			private readonly int layoutId;
		}

		protected internal abstract void Convert(Com.Zhy.Adapter.Recyclerview.Base.ViewHolder
			 holder, T t, int position);
	}
}
