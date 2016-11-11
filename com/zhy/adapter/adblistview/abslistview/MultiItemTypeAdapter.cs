using BaseAdapterSharp;


namespace Com.Zhy.Adapter.Abslistview
{
	public class MultiItemTypeAdapter<T> : Android.Widget.BaseAdapter
	{
		protected internal Android.Content.Context mContext;

		protected internal System.Collections.Generic.IList<T> mDatas;

		private Base.ItemViewDelegateManager<T> mItemViewDelegateManager;

		public MultiItemTypeAdapter(Android.Content.Context context, System.Collections.Generic.IList<T> datas)
		{
			this.mContext = context;
			this.mDatas = datas;
			mItemViewDelegateManager = new Base.ItemViewDelegateManager<T>();
		}

		public virtual MultiItemTypeAdapter<T> AddItemViewDelegate(Base.ItemViewDelegate<T> itemViewDelegate)
		{
			mItemViewDelegateManager.AddDelegate(itemViewDelegate);
			return this;
		}

		private bool UseItemViewDelegateManager()
		{
			return mItemViewDelegateManager.GetItemViewDelegateCount() > 0;
		}

		public override int ViewTypeCount
		{
			get
			{
				if (UseItemViewDelegateManager())
				{
					return mItemViewDelegateManager.GetItemViewDelegateCount();
				}
				return base.ViewTypeCount;
			}

		}

		public override int GetItemViewType(int position)
		{
			if (UseItemViewDelegateManager())
			{
				int viewType = mItemViewDelegateManager.GetItemViewType(mDatas[position], position
					);
				return viewType;
			}
			return base.GetItemViewType(position);
		}

		public override Android.Views.View GetView(int position, Android.Views.View convertView
			, Android.Views.ViewGroup parent)
		{
			Com.Zhy.Adapter.Abslistview.Base.ItemViewDelegate<T> itemViewDelegate = mItemViewDelegateManager
				.GetItemViewDelegate(mDatas[position], position);
			int layoutId = itemViewDelegate.GetItemViewLayoutId();
			Com.Zhy.Adapter.Abslistview.ViewHolder viewHolder = null;
			if (convertView == null)
			{
				Android.Views.View itemView = Android.Views.LayoutInflater.From(mContext).Inflate
					(layoutId, parent, false);
				viewHolder = new Com.Zhy.Adapter.Abslistview.ViewHolder(mContext, itemView, parent
					, position);
				viewHolder.mLayoutId = layoutId;
				OnViewHolderCreated(viewHolder, viewHolder.GetConvertView());
			}
			else
			{
				viewHolder = (ViewHolder)convertView.Tag;
				viewHolder.mPosition = position;
			}
			Jbox<T> box = (Jbox<T>)GetItem(position);
			Convert(viewHolder, box.Value, position);
			return viewHolder.GetConvertView();
		}

		protected internal virtual void Convert(ViewHolder viewHolder
			, T item, int position)
		{
			mItemViewDelegateManager.Convert(viewHolder, item, position);
		}

		public virtual void OnViewHolderCreated(ViewHolder holder
			, Android.Views.View itemView)
		{
		}

		public override int Count
		{
			get
			{
				return mDatas.Count;
			}

		}

		public override Java.Lang.Object GetItem(int position)
		{
			return new Jbox<T>(mDatas[position]);
		}

		public override long GetItemId(int position)
		{
			return position;
		}
	}
}
