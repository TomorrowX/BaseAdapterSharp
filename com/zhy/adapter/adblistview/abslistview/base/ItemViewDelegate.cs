

namespace Com.Zhy.Adapter.Abslistview.Base
{
	/// <summary>Created by zhy on 16/6/22.</summary>
	public interface ItemViewDelegate<T>
	{
		int GetItemViewLayoutId();

		bool IsForViewType(T item, int position);

		void Convert(Com.Zhy.Adapter.Abslistview.ViewHolder holder, T t, int position);
	}
}
