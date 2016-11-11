namespace Com.Zhy.Adapter.Abslistview
{
	public abstract class CommonAdapter<T> : MultiItemTypeAdapter<T>
	{
		public CommonAdapter(Android.Content.Context context, int layoutId, System.Collections.Generic.IList<T> datas) : base(context, datas)
		{
			AddItemViewDelegate(new _ItemViewDelegate_18(this, layoutId));
		}

		sealed class _ItemViewDelegate_18 : Base.ItemViewDelegate<T>
		{
			public _ItemViewDelegate_18(CommonAdapter<T> _enclosing, int layoutId)
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

			public void Convert(ViewHolder holder, T t, int position)
			{
				_enclosing.Convert(holder, t, position);
			}

			readonly CommonAdapter<T> _enclosing;

			readonly int layoutId;
		}

		protected internal abstract override void Convert(ViewHolder viewHolder, T item, int position);
	}
}
