

namespace Com.Zhy.Adapter.Recyclerview.Utils
{
	/// <summary>Created by zhy on 16/6/28.</summary>
	public class WrapperUtils
	{
		public interface SpanSizeCallback
		{
			int GetSpanSize(Android.Support.V7.Widget.GridLayoutManager layoutManager, Android.Support.V7.Widget.GridLayoutManager.SpanSizeLookup
				 oldLookup, int position);
		}

		public static void OnAttachedToRecyclerView(Android.Support.V7.Widget.RecyclerView.Adapter
			 innerAdapter, Android.Support.V7.Widget.RecyclerView recyclerView, Com.Zhy.Adapter.Recyclerview.Utils.WrapperUtils.SpanSizeCallback
			 callback)
		{
			innerAdapter.OnAttachedToRecyclerView(recyclerView);
			Android.Support.V7.Widget.RecyclerView.LayoutManager layoutManager = recyclerView
				.GetLayoutManager();
			if (layoutManager is Android.Support.V7.Widget.GridLayoutManager)
			{
				Android.Support.V7.Widget.GridLayoutManager gridLayoutManager = (Android.Support.V7.Widget.GridLayoutManager
					)layoutManager;
				Android.Support.V7.Widget.GridLayoutManager.SpanSizeLookup spanSizeLookup = gridLayoutManager
					.GetSpanSizeLookup();
				gridLayoutManager.SetSpanSizeLookup(new _SpanSizeLookup_29(callback, gridLayoutManager
					, spanSizeLookup));
				gridLayoutManager.SpanCount = (gridLayoutManager.SpanCount);
			}
		}

		private sealed class _SpanSizeLookup_29 : Android.Support.V7.Widget.GridLayoutManager.SpanSizeLookup
		{
			public _SpanSizeLookup_29(Com.Zhy.Adapter.Recyclerview.Utils.WrapperUtils.SpanSizeCallback
				 callback, Android.Support.V7.Widget.GridLayoutManager gridLayoutManager, Android.Support.V7.Widget.GridLayoutManager.SpanSizeLookup
				 spanSizeLookup)
			{
				this.callback = callback;
				this.gridLayoutManager = gridLayoutManager;
				this.spanSizeLookup = spanSizeLookup;
			}

			public override int GetSpanSize(int position)
			{
				return callback.GetSpanSize(gridLayoutManager, spanSizeLookup, position);
			}

			private readonly Com.Zhy.Adapter.Recyclerview.Utils.WrapperUtils.SpanSizeCallback
				 callback;

			private readonly Android.Support.V7.Widget.GridLayoutManager gridLayoutManager;

			private readonly Android.Support.V7.Widget.GridLayoutManager.SpanSizeLookup spanSizeLookup;
		}

		public static void SetFullSpan(Android.Support.V7.Widget.RecyclerView.ViewHolder 
			holder)
		{
			Android.Views.ViewGroup.LayoutParams lp = holder.ItemView.LayoutParameters;
			if (lp != null && lp is Android.Support.V7.Widget.StaggeredGridLayoutManager.LayoutParams)
			{
				Android.Support.V7.Widget.StaggeredGridLayoutManager.LayoutParams p = (Android.Support.V7.Widget.StaggeredGridLayoutManager.LayoutParams
					)lp;
				p.FullSpan = (true);
			}
		}
	}
}
