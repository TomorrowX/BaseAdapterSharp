

namespace Com.Zhy.Adapter.Recyclerview.Base
{
	/// <summary>Created by zhy on 16/6/22.</summary>
	public interface ItemViewDelegate<T>
	{
		int GetItemViewLayoutId();

		bool IsForViewType(T item, int position);

		void Convert(ViewHolder holder, T t, int position);
	}
}
