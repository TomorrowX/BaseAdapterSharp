

namespace Com.Zhy.Adapter.Recyclerview.Wrapper
{
	/// <summary>Created by zhy on 16/6/23.</summary>
	public class HeaderAndFooterWrapper<T> : Android.Support.V7.Widget.RecyclerView.Adapter
	{
		private const int BaseItemTypeHeader = 100000;

		private const int BaseItemTypeFooter = 200000;

		private Android.Util.SparseArray<Android.Views.View> mHeaderViews
					   = new Android.Util.SparseArray<Android.Views.View>();

		private Android.Util.SparseArray<Android.Views.View> mFootViews = 
			new Android.Util.SparseArray<Android.Views.View>();

		private Android.Support.V7.Widget.RecyclerView.Adapter mInnerAdapter;

		public HeaderAndFooterWrapper(Android.Support.V7.Widget.RecyclerView.Adapter adapter)
		{
			mInnerAdapter = adapter;
		}

		public override Android.Support.V7.Widget.RecyclerView.ViewHolder OnCreateViewHolder(Android.Views.ViewGroup parent, int viewType)
		{
			if (mHeaderViews.Get(viewType) != null)
			{
				Com.Zhy.Adapter.Recyclerview.Base.ViewHolder holder = Com.Zhy.Adapter.Recyclerview.Base.ViewHolder
					.CreateViewHolder(parent.Context, mHeaderViews.Get(viewType));
				return holder;
			}
			else
			{
				if (mFootViews.Get(viewType) != null)
				{
					Com.Zhy.Adapter.Recyclerview.Base.ViewHolder holder = Com.Zhy.Adapter.Recyclerview.Base.ViewHolder
						.CreateViewHolder(parent.Context, mFootViews.Get(viewType));
					return holder;
				}
			}
			return mInnerAdapter.OnCreateViewHolder(parent, viewType);
		}

		public override int GetItemViewType(int position)
		{
			if (IsHeaderViewPos(position))
			{
				return mHeaderViews.KeyAt(position);
			}
			else
			{
				if (IsFooterViewPos(position))
				{
					return mFootViews.KeyAt(position - GetHeadersCount() - GetRealItemCount());
				}
			}
			return mInnerAdapter.GetItemViewType(position - GetHeadersCount());
		}

		private int GetRealItemCount()
		{
			return mInnerAdapter.ItemCount;
		}

		public override void OnBindViewHolder(Android.Support.V7.Widget.RecyclerView.ViewHolder holder, int position)
		{
			if (IsHeaderViewPos(position))
			{
				return;
			}
			if (IsFooterViewPos(position))
			{
				return;
			}
			mInnerAdapter.OnBindViewHolder(holder, position - GetHeadersCount());
		}

		public override int ItemCount
		{
			get
			{
				return GetHeadersCount() + GetFootersCount() + GetRealItemCount();
			}
		}

		public override void OnAttachedToRecyclerView(Android.Support.V7.Widget.RecyclerView recyclerView)
		{
			Com.Zhy.Adapter.Recyclerview.Utils.WrapperUtils.OnAttachedToRecyclerView(mInnerAdapter
				, recyclerView, new _SpanSizeCallback_90(this));
		}

		private sealed class _SpanSizeCallback_90 : Com.Zhy.Adapter.Recyclerview.Utils.WrapperUtils.SpanSizeCallback
		{
			public _SpanSizeCallback_90(HeaderAndFooterWrapper<T> _enclosing)
			{
				this._enclosing = _enclosing;
			}

			public int GetSpanSize(Android.Support.V7.Widget.GridLayoutManager layoutManager, 
				Android.Support.V7.Widget.GridLayoutManager.SpanSizeLookup oldLookup, int position
				)
			{
				int viewType = this._enclosing.GetItemViewType(position);
				if (this._enclosing.mHeaderViews.Get(viewType) != null)
				{
					return layoutManager.SpanCount;
				}
				else
				{
					if (this._enclosing.mFootViews.Get(viewType) != null)
					{
						return layoutManager.SpanCount;
					}
				}
				if (oldLookup != null)
				{
					return oldLookup.GetSpanSize(position);
				}
				return 1;
			}

			private readonly HeaderAndFooterWrapper<T> _enclosing;
		}

		public override void OnViewAttachedToWindow(Java.Lang.Object obj)
		{
			Android.Support.V7.Widget.RecyclerView.ViewHolder holder = (Android.Support.V7.Widget.RecyclerView.ViewHolder)obj;
			mInnerAdapter.OnViewAttachedToWindow(holder);
			int position = holder.LayoutPosition;
			if (IsHeaderViewPos(position) || IsFooterViewPos(position))
			{
				Com.Zhy.Adapter.Recyclerview.Utils.WrapperUtils.SetFullSpan(holder);
			}
		}

		private bool IsHeaderViewPos(int position)
		{
			return position < GetHeadersCount();
		}

		private bool IsFooterViewPos(int position)
		{
			return position >= GetHeadersCount() + GetRealItemCount();
		}

		public virtual void AddHeaderView(Android.Views.View view)
		{
			mHeaderViews.Put(mHeaderViews.Size() + BaseItemTypeHeader, view);
		}

		public virtual void AddFootView(Android.Views.View view)
		{
			mFootViews.Put(mFootViews.Size() + BaseItemTypeFooter, view);
		}

		public virtual int GetHeadersCount()
		{
			return mHeaderViews.Size();
		}

		public virtual int GetFootersCount()
		{
			return mFootViews.Size();
		}
	}
}
