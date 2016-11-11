

namespace Com.Zhy.Adapter.Recyclerview.Wrapper
{
	/// <summary>Created by zhy on 16/6/23.</summary>
	public class LoadMoreWrapper<T> : Android.Support.V7.Widget.RecyclerView.Adapter
	{
		public const int ItemTypeLoadMore = int.MaxValue - 2;

		private Android.Support.V7.Widget.RecyclerView.Adapter mInnerAdapter;

		private Android.Views.View mLoadMoreView;

		private int mLoadMoreLayoutId;

		public LoadMoreWrapper(Android.Support.V7.Widget.RecyclerView.Adapter adapter)
		{
			mInnerAdapter = adapter;
		}

		private bool HasLoadMore()
		{
			return mLoadMoreView != null || mLoadMoreLayoutId != 0;
		}

		private bool IsShowLoadMore(int position)
		{
			return HasLoadMore() && (position >= mInnerAdapter.ItemCount);
		}

		public override int GetItemViewType(int position)
		{
			if (IsShowLoadMore(position))
			{
				return ItemTypeLoadMore;
			}
			return mInnerAdapter.GetItemViewType(position);
		}

		public override Android.Support.V7.Widget.RecyclerView.ViewHolder OnCreateViewHolder(Android.Views.ViewGroup parent, int viewType)
		{
			if (viewType == ItemTypeLoadMore)
			{
				Com.Zhy.Adapter.Recyclerview.Base.ViewHolder holder;
				if (mLoadMoreView != null)
				{
					holder = Com.Zhy.Adapter.Recyclerview.Base.ViewHolder.CreateViewHolder(parent.Context, mLoadMoreView);
				}
				else
				{
					holder = Com.Zhy.Adapter.Recyclerview.Base.ViewHolder.CreateViewHolder(parent.Context, parent, mLoadMoreLayoutId);
				}
				return holder;
			}
			return mInnerAdapter.OnCreateViewHolder(parent, viewType);
		}

		public override void OnBindViewHolder(Android.Support.V7.Widget.RecyclerView.ViewHolder holder, int position)
		{
			if (IsShowLoadMore(position))
			{
				if (mOnLoadMoreListener != null)
				{
					mOnLoadMoreListener.OnLoadMoreRequested();
				}
				return;
			}
			mInnerAdapter.OnBindViewHolder(holder, position);
		}

		public override void OnAttachedToRecyclerView(Android.Support.V7.Widget.RecyclerView recyclerView)
		{
			Com.Zhy.Adapter.Recyclerview.Utils.WrapperUtils.OnAttachedToRecyclerView(mInnerAdapter
				, recyclerView, new _SpanSizeCallback_86(this));
		}

		private sealed class _SpanSizeCallback_86 : Com.Zhy.Adapter.Recyclerview.Utils.WrapperUtils.SpanSizeCallback
		{
			public _SpanSizeCallback_86(LoadMoreWrapper<T> _enclosing)
			{
				this._enclosing = _enclosing;
			}

			public int GetSpanSize(Android.Support.V7.Widget.GridLayoutManager layoutManager, 
				Android.Support.V7.Widget.GridLayoutManager.SpanSizeLookup oldLookup, int position
				)
			{
				if (this._enclosing.IsShowLoadMore(position))
				{
					return layoutManager.SpanCount;
				}
				if (oldLookup != null)
				{
					return oldLookup.GetSpanSize(position);
				}
				return 1;
			}

			private readonly LoadMoreWrapper<T> _enclosing;
		}

		public override void OnViewAttachedToWindow(Java.Lang.Object obj )
		{
			Android.Support.V7.Widget.RecyclerView.ViewHolder holder = (Android.Support.V7.Widget.RecyclerView.ViewHolder)obj;
			mInnerAdapter.OnViewAttachedToWindow(holder);
			if (IsShowLoadMore(holder.LayoutPosition))
			{
				SetFullSpan(holder);
			}
		}

		private void SetFullSpan(Android.Support.V7.Widget.RecyclerView.ViewHolder holder
			)
		{
			Android.Views.ViewGroup.LayoutParams lp = holder.ItemView.LayoutParameters;
			if (lp != null && lp is Android.Support.V7.Widget.StaggeredGridLayoutManager.LayoutParams)
			{
				Android.Support.V7.Widget.StaggeredGridLayoutManager.LayoutParams p = (Android.Support.V7.Widget.StaggeredGridLayoutManager.LayoutParams
					)lp;
				p.FullSpan = (true);
			}
		}

		public override int ItemCount
		{
			get
			{
				return mInnerAdapter.ItemCount + (HasLoadMore() ? 1 : 0);
			}
		}

		public interface OnLoadMoreListener
		{
			void OnLoadMoreRequested();
		}

		private OnLoadMoreListener mOnLoadMoreListener;

		public virtual Wrapper.LoadMoreWrapper<T> SetOnLoadMoreListener(OnLoadMoreListener loadMoreListener)
		{
			if (loadMoreListener != null)
			{
				mOnLoadMoreListener = loadMoreListener;
			}
			return this;
		}

		public virtual LoadMoreWrapper<T> SetLoadMoreView(Android.Views.View loadMoreView)
		{
			mLoadMoreView = loadMoreView;
			return this;
		}

		public virtual LoadMoreWrapper<T> SetLoadMoreView(int layoutId)
		{
			mLoadMoreLayoutId = layoutId;
			return this;
		}
	}
}
