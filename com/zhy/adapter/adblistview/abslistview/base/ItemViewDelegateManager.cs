

namespace Com.Zhy.Adapter.Abslistview.Base
{
	/// <summary>Created by zhy on 16/6/22.</summary>
	public class ItemViewDelegateManager<T>
	{
		internal Android.Util.SparseArray<ItemViewDelegate<T>> delegates = new Android.Util.SparseArray<ItemViewDelegate<T>>();

		public virtual int GetItemViewDelegateCount()
		{
			return delegates.Size();
		}

		public virtual Com.Zhy.Adapter.Abslistview.Base.ItemViewDelegateManager<T> AddDelegate
			(Com.Zhy.Adapter.Abslistview.Base.ItemViewDelegate<T> delegate_)
		{
			int viewType = delegates.Size();
			if (delegate_ != null)
			{
				delegates.Put(viewType, delegate_);
				viewType++;
			}
			return this;
		}

		public virtual Com.Zhy.Adapter.Abslistview.Base.ItemViewDelegateManager<T> AddDelegate
			(int viewType, Com.Zhy.Adapter.Abslistview.Base.ItemViewDelegate<T> delegate_)
		{
			if (delegates.Get(viewType) != null)
			{
				throw new System.ArgumentException("An ItemViewDelegate is already registered for the viewType = "
					 + viewType + ". Already registered ItemViewDelegate is " + delegates.Get(viewType
					));
			}
			delegates.Put(viewType, delegate_);
			return this;
		}

		public virtual Com.Zhy.Adapter.Abslistview.Base.ItemViewDelegateManager<T> RemoveDelegate
			(Com.Zhy.Adapter.Abslistview.Base.ItemViewDelegate<T> delegate_)
		{
			if (delegate_ == null)
			{
				throw new System.ArgumentNullException("ItemViewDelegate is null");
			}
			int indexToRemove = delegates.IndexOfValue(delegate_);
			if (indexToRemove >= 0)
			{
				delegates.RemoveAt(indexToRemove);
			}
			return this;
		}

		public virtual Com.Zhy.Adapter.Abslistview.Base.ItemViewDelegateManager<T> RemoveDelegate
			(int itemType)
		{
			int indexToRemove = delegates.IndexOfKey(itemType);
			if (indexToRemove >= 0)
			{
				delegates.RemoveAt(indexToRemove);
			}
			return this;
		}

		public virtual int GetItemViewType(T item, int position)
		{
			int delegatesCount = delegates.Size();
			for (int i = delegatesCount - 1; i >= 0; i--)
			{
				Com.Zhy.Adapter.Abslistview.Base.ItemViewDelegate<T> delegate_ = delegates.ValueAt
					(i);
				if (delegate_.IsForViewType(item, position))
				{
					return delegates.KeyAt(i);
				}
			}
			throw new System.ArgumentException("No ItemViewDelegate added that matches position="
				 + position + " in data source");
		}

		public virtual void Convert(Com.Zhy.Adapter.Abslistview.ViewHolder holder, T item
			, int position)
		{
			int delegatesCount = delegates.Size();
			for (int i = 0; i < delegatesCount; i++)
			{
				Com.Zhy.Adapter.Abslistview.Base.ItemViewDelegate<T> delegate_ = delegates.ValueAt
					(i);
				if (delegate_.IsForViewType(item, position))
				{
					delegate_.Convert(holder, item, position);
					return;
				}
			}
			throw new System.ArgumentException("No ItemViewDelegateManager added that matches position="
				 + position + " in data source");
		}

		public virtual int GetItemViewLayoutId(int viewType)
		{
			return delegates.Get(viewType).GetItemViewLayoutId();
		}

		public virtual int GetItemViewType(ItemViewDelegate<T>
			 itemViewDelegate)
		{
			return delegates.IndexOfValue(itemViewDelegate);
		}

		public virtual ItemViewDelegate<T> GetItemViewDelegate
			(T item, int position)
		{
			int delegatesCount = delegates.Size();
			for (int i = delegatesCount - 1; i >= 0; i--)
			{
				ItemViewDelegate<T> delegate_ = delegates.ValueAt(i);
				if (delegate_.IsForViewType(item, position))
				{
					return delegate_;
				}
			}
			throw new System.ArgumentException("No ItemViewDelegate added that matches position="
				 + position + " in data source");
		}

		public virtual int GetItemViewLayoutId(T item, int position)
		{
			return GetItemViewDelegate(item, position).GetItemViewLayoutId();
		}
	}
}
