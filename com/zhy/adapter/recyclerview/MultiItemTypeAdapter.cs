

namespace Com.Zhy.Adapter.Recyclerview
{
	/// <summary>Created by zhy on 16/4/9.</summary>
	public class MultiItemTypeAdapter<T> : Android.Support.V7.Widget.RecyclerView.Adapter
	{
		protected internal Android.Content.Context mContext;

		protected internal System.Collections.Generic.IList<T> mDatas;

		protected internal Base.ItemViewDelegateManager<T> mItemViewDelegateManager;

		protected internal OnItemClickListener
			 mOnItemClickListener;

		public MultiItemTypeAdapter(Android.Content.Context context, System.Collections.Generic.IList<T> datas)
		{
			mContext = context;
			mDatas = datas;
			mItemViewDelegateManager = new Com.Zhy.Adapter.Recyclerview.Base.ItemViewDelegateManager<T>();
		}

		public override int GetItemViewType(int position)
		{
			if (!UseItemViewDelegateManager())
			{
				return base.GetItemViewType(position);
			}
			return mItemViewDelegateManager.GetItemViewType(mDatas[position], position);
		}

		public override Android.Support.V7.Widget.RecyclerView.ViewHolder OnCreateViewHolder(Android.Views.ViewGroup
			 parent, int viewType)
		{
			Com.Zhy.Adapter.Recyclerview.Base.ItemViewDelegate<T> itemViewDelegate = mItemViewDelegateManager
				.GetItemViewDelegate(viewType);
			int layoutId = itemViewDelegate.GetItemViewLayoutId();
			Com.Zhy.Adapter.Recyclerview.Base.ViewHolder holder = Com.Zhy.Adapter.Recyclerview.Base.ViewHolder
				.CreateViewHolder(mContext, parent, layoutId);
			OnViewHolderCreated(holder, holder.GetConvertView());
			SetListener(parent, holder, viewType);
			return holder;
		}

		public virtual void OnViewHolderCreated(Com.Zhy.Adapter.Recyclerview.Base.ViewHolder
			 holder, Android.Views.View itemView)
		{
		}

		public virtual void Convert(Com.Zhy.Adapter.Recyclerview.Base.ViewHolder holder, 
			T t)
		{
			mItemViewDelegateManager.Convert(holder, t, holder.AdapterPosition);
		}

		protected internal virtual bool IsEnabled(int viewType)
		{
			return true;
		}

		protected internal virtual void SetListener(Android.Views.ViewGroup parent, Com.Zhy.Adapter.Recyclerview.Base.ViewHolder
			 viewHolder, int viewType)
		{
			if (!IsEnabled(viewType))
			{
				return;
			}
			viewHolder.GetConvertView().SetOnClickListener(new _OnClickListener_63(this, viewHolder));
			viewHolder.GetConvertView().SetOnLongClickListener(new _OnLongClickListener_73(this
				, viewHolder));
		}

		private sealed class _OnClickListener_63 :Java.Lang.Object, Android.Views.View.IOnClickListener
		{
			public _OnClickListener_63(MultiItemTypeAdapter<T> _enclosing, Com.Zhy.Adapter.Recyclerview.Base.ViewHolder
				 viewHolder)
			{
				this._enclosing = _enclosing;
				this.viewHolder = viewHolder;
			}

			public void OnClick(Android.Views.View v)
			{
				if (this._enclosing.mOnItemClickListener != null)
				{
					int position = viewHolder.AdapterPosition;
					this._enclosing.mOnItemClickListener.OnItemClick(v, viewHolder, position);
				}
			}

			private readonly MultiItemTypeAdapter<T> _enclosing;

			private readonly Com.Zhy.Adapter.Recyclerview.Base.ViewHolder viewHolder;
		}

		private sealed class _OnLongClickListener_73 :Java.Lang.Object,Android.Views.View.IOnLongClickListener
		{
			public _OnLongClickListener_73(MultiItemTypeAdapter<T> _enclosing, Com.Zhy.Adapter.Recyclerview.Base.ViewHolder
				 viewHolder)
			{
				this._enclosing = _enclosing;
				this.viewHolder = viewHolder;
			}

			public bool OnLongClick(Android.Views.View v)
			{
				if (this._enclosing.mOnItemClickListener != null)
				{
					int position = viewHolder.AdapterPosition;
					return this._enclosing.mOnItemClickListener.OnItemLongClick(v, viewHolder, position
						);
				}
				return false;
			}

			private readonly MultiItemTypeAdapter<T> _enclosing;

			private readonly Com.Zhy.Adapter.Recyclerview.Base.ViewHolder viewHolder;
		}

		public override void OnBindViewHolder(Android.Support.V7.Widget.RecyclerView.ViewHolder holder, int position)
		{
			Base.ViewHolder h = (Com.Zhy.Adapter.Recyclerview.Base.ViewHolder)holder;
			Convert(h, mDatas[position]);
		}

		public override int ItemCount
		{
			get
			{
				int itemCount = mDatas.Count;
				return itemCount;
			}

		}

		public virtual System.Collections.Generic.IList<T> GetDatas()
		{
			return mDatas;
		}

		public virtual Com.Zhy.Adapter.Recyclerview.MultiItemTypeAdapter<T> AddItemViewDelegate
			(Com.Zhy.Adapter.Recyclerview.Base.ItemViewDelegate<T> itemViewDelegate)
		{
			mItemViewDelegateManager.AddDelegate(itemViewDelegate);
			return this;
		}

		public virtual Com.Zhy.Adapter.Recyclerview.MultiItemTypeAdapter<T> AddItemViewDelegate
			(int viewType, Com.Zhy.Adapter.Recyclerview.Base.ItemViewDelegate<T> itemViewDelegate
			)
		{
			mItemViewDelegateManager.AddDelegate(viewType, itemViewDelegate);
			return this;
		}

		protected internal virtual bool UseItemViewDelegateManager()
		{
			return mItemViewDelegateManager.GetItemViewDelegateCount() > 0;
		}

		public interface OnItemClickListener
		{
			void OnItemClick(Android.Views.View view, Android.Support.V7.Widget.RecyclerView.ViewHolder
				 holder, int position);

			bool OnItemLongClick(Android.Views.View view, Android.Support.V7.Widget.RecyclerView.ViewHolder
				 holder, int position);
		}

		public virtual void SetOnItemClickListener(OnItemClickListener
			 onItemClickListener)
		{
			this.mOnItemClickListener = onItemClickListener;
		}
	}
}
