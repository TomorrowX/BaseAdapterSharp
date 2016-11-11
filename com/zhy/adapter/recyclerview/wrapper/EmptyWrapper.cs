

namespace Com.Zhy.Adapter.Recyclerview.Wrapper
{
	/// <summary>Created by zhy on 16/6/23.</summary>
	public class EmptyWrapper<T> : Android.Support.V7.Widget.RecyclerView.Adapter
	{
		public const int ItemTypeEmpty = int.MaxValue - 1;

		private Android.Support.V7.Widget.RecyclerView.Adapter mInnerAdapter;

		private Android.Views.View mEmptyView;

		private int mEmptyLayoutId;

		public EmptyWrapper(Android.Support.V7.Widget.RecyclerView.Adapter adapter)
		{
			mInnerAdapter = adapter;
		}

		private bool IsEmpty()
		{
			return (mEmptyView != null || mEmptyLayoutId != 0) && mInnerAdapter.ItemCount == 0;
		}

		public override Android.Support.V7.Widget.RecyclerView.ViewHolder OnCreateViewHolder(Android.Views.ViewGroup parent, int viewType)
		{
			if (IsEmpty())
			{
				Com.Zhy.Adapter.Recyclerview.Base.ViewHolder holder;
				if (mEmptyView != null)
				{
					holder = Com.Zhy.Adapter.Recyclerview.Base.ViewHolder.CreateViewHolder(parent.Context, mEmptyView);
				}
				else
				{
					holder = Com.Zhy.Adapter.Recyclerview.Base.ViewHolder.CreateViewHolder(parent.Context, parent, mEmptyLayoutId);
				}
				return holder;
			}
			return mInnerAdapter.OnCreateViewHolder(parent, viewType);
		}

		public override void OnAttachedToRecyclerView(Android.Support.V7.Widget.RecyclerView recyclerView)
		{
			Com.Zhy.Adapter.Recyclerview.Utils.WrapperUtils.OnAttachedToRecyclerView(mInnerAdapter
				, recyclerView, new _SpanSizeCallback_56(this));
		}

		private sealed class _SpanSizeCallback_56 : Com.Zhy.Adapter.Recyclerview.Utils.WrapperUtils.SpanSizeCallback
		{
			public _SpanSizeCallback_56(EmptyWrapper<T> _enclosing)
			{
				this._enclosing = _enclosing;
			}

			public int GetSpanSize(Android.Support.V7.Widget.GridLayoutManager gridLayoutManager
				, Android.Support.V7.Widget.GridLayoutManager.SpanSizeLookup oldLookup, int position
				)
			{
				if (this._enclosing.IsEmpty())
				{
					return gridLayoutManager.SpanCount;
				}
				if (oldLookup != null)
				{
					return oldLookup.GetSpanSize(position);
				}
				return 1;
			}

			private readonly EmptyWrapper<T> _enclosing;
		}


		public override void OnViewAttachedToWindow(Java.Lang.Object holder)
		{
			mInnerAdapter.OnViewAttachedToWindow(holder);
			if (IsEmpty())
			{
				Com.Zhy.Adapter.Recyclerview.Utils.WrapperUtils.SetFullSpan((Android.Support.V7.Widget.RecyclerView.ViewHolder)holder);
			}
		}

		public override int GetItemViewType(int position)
		{
			if (IsEmpty())
			{
				return ItemTypeEmpty;
			}
			return mInnerAdapter.GetItemViewType(position);
		}

		public override void OnBindViewHolder(Android.Support.V7.Widget.RecyclerView.ViewHolder
			 holder, int position)
		{
			if (IsEmpty())
			{
				return;
			}
			mInnerAdapter.OnBindViewHolder(holder, position);
		}

		public override int ItemCount
		{
			get
			{
				if (IsEmpty())
				{
					return 1;
				}
				return mInnerAdapter.ItemCount;
			}
		}

		public virtual void SetEmptyView(Android.Views.View emptyView)
		{
			mEmptyView = emptyView;
		}

		public virtual void SetEmptyView(int layoutId)
		{
			mEmptyLayoutId = layoutId;
		}
	}
}
