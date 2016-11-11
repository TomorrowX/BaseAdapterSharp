

using System;
using Android.Views;
using Android.Widget;
using Java.Lang;
using BaseAdapterSharp;

namespace Com.Zhy.Adapter.Abslistview
{
	public class ViewHolder
	{
		private Android.Util.SparseArray<Android.Views.View> mViews;

		protected internal int mPosition;

		private Android.Views.View mConvertView;

		private Android.Content.Context mContext;

		protected internal int mLayoutId;

		public ViewHolder(Android.Content.Context context, Android.Views.View itemView, Android.Views.ViewGroup parent, int position)
		{
			mContext = context;
			mConvertView = itemView;
			mPosition = position;
			mViews = new Android.Util.SparseArray<Android.Views.View>();
			mConvertView.Tag = new Jbox<ViewHolder>(this);
		}

		public static Com.Zhy.Adapter.Abslistview.ViewHolder Get(Android.Content.Context 
			context, Android.Views.View convertView, Android.Views.ViewGroup parent, int layoutId
			, int position)
		{
			if (convertView == null)
			{
				Android.Views.View itemView = Android.Views.LayoutInflater.From(context).Inflate(
					layoutId, parent, false);
				Com.Zhy.Adapter.Abslistview.ViewHolder holder = new Com.Zhy.Adapter.Abslistview.ViewHolder
					(context, itemView, parent, position);
				holder.mLayoutId = layoutId;
				return holder;
			}
			else
			{
				ViewHolder holder = ((Jbox<ViewHolder>)convertView.Tag).Value;
				holder.mPosition = position;
				return holder;
			}
		}

		/// <summary>通过viewId获取控件</summary>
		/// <param name="viewId"/>
		/// <returns/>
		public virtual T GetView<T>(int viewId)
			where T : Android.Views.View
		{
			Android.Views.View view = mViews.Get(viewId);
			if (view == null)
			{
				view = mConvertView.FindViewById(viewId);
				mViews.Put(viewId, view);
			}
			return (T)view;
		}

		public virtual Android.Views.View GetConvertView()
		{
			return mConvertView;
		}

		public virtual int GetLayoutId()
		{
			return mLayoutId;
		}

		public virtual void UpdatePosition(int position)
		{
			mPosition = position;
		}

		public virtual int GetItemPosition()
		{
			return mPosition;
		}

		/// <summary>设置TextView的值</summary>
		/// <param name="viewId"/>
		/// <param name="text"/>
		/// <returns/>
		public virtual Com.Zhy.Adapter.Abslistview.ViewHolder SetText(int viewId, string 
			text)
		{
			Android.Widget.TextView tv = GetView<TextView>(viewId);
			tv.Text = (text);
			return this;
		}

		public static explicit operator ViewHolder(Java.Lang.Object v)
		{
			throw new NotImplementedException();
		}

		public virtual Com.Zhy.Adapter.Abslistview.ViewHolder SetImageResource(int viewId
			, int resId)
		{
			Android.Widget.ImageView view = GetView<ImageView>(viewId);
			view.SetImageResource(resId);
			return this;
		}

		public virtual Com.Zhy.Adapter.Abslistview.ViewHolder SetImageBitmap(int viewId, 
			Android.Graphics.Bitmap bitmap)
		{
			Android.Widget.ImageView view = GetView<ImageView>(viewId);
			view.SetImageBitmap(bitmap);
			return this;
		}

		public virtual Com.Zhy.Adapter.Abslistview.ViewHolder SetImageDrawable(int viewId
			, Android.Graphics.Drawables.Drawable drawable)
		{
			Android.Widget.ImageView view = GetView<ImageView>(viewId);
			view.SetImageDrawable(drawable);
			return this;
		}

		public virtual Com.Zhy.Adapter.Abslistview.ViewHolder SetBackgroundColor(int viewId
			, int color)
		{
			Android.Views.View view = GetView<View>(viewId);
			view.SetBackgroundColor(new Android.Graphics.Color(color));
			return this;
		}

		public virtual Com.Zhy.Adapter.Abslistview.ViewHolder SetBackgroundRes(int viewId
			, int backgroundRes)
		{
			Android.Views.View view = GetView<View>(viewId);
			view.SetBackgroundResource(backgroundRes);
			return this;
		}

		public virtual Com.Zhy.Adapter.Abslistview.ViewHolder SetTextColor(int viewId, int
			 textColor)
		{
			Android.Widget.TextView view = GetView<TextView>(viewId);
			view.SetTextColor( Android.Content.Res.ColorStateList.ValueOf(new Android.Graphics.Color(textColor)));
			return this;
		}

		public virtual Com.Zhy.Adapter.Abslistview.ViewHolder SetTextColorRes(int viewId, 
			int textColorRes)
		{
			Android.Widget.TextView view = GetView<TextView>(viewId);
#pragma warning disable CS0618 // Type or member is obsolete
			view.SetTextColor(mContext.Resources.GetColor(textColorRes));
#pragma warning restore CS0618 // Type or member is obsolete
			return this;
		}

		public virtual Com.Zhy.Adapter.Abslistview.ViewHolder SetAlpha(int viewId, float 
			value)
		{
			if (Android.OS.Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.Honeycomb)
			{
				GetView<View>(viewId).Alpha = (value);
			}
			else
			{
				// Pre-honeycomb hack to set Alpha value
				Android.Views.Animations.AlphaAnimation alpha = new Android.Views.Animations.AlphaAnimation
					(value, value);
				alpha.Duration=(0);
				alpha.FillAfter = (true);
				GetView<View>(viewId).StartAnimation(alpha);
			}
			return this;
		}

		public virtual Com.Zhy.Adapter.Abslistview.ViewHolder SetVisible(int viewId, bool
			 visible)
		{
			Android.Views.View view = GetView<View>(viewId);
			view.Visibility = (visible ?  ViewStates.Visible : ViewStates.Gone);
			return this;
		}

		public virtual Com.Zhy.Adapter.Abslistview.ViewHolder Linkify(int viewId)
		{
			Android.Widget.TextView view = GetView<TextView>(viewId);
			Android.Text.Util.Linkify.AddLinks(view,  Android.Text.Util.MatchOptions.All);
			return this;
		}

		public virtual Com.Zhy.Adapter.Abslistview.ViewHolder SetTypeface(Android.Graphics.Typeface
			 typeface, params int[] viewIds)
		{
			foreach (int viewId in viewIds)
			{
				Android.Widget.TextView view = GetView<TextView>(viewId);
				view.Typeface = typeface;
				view.PaintFlags = (view.PaintFlags | Android.Graphics.PaintFlags.SubpixelText);
			}
			return this;
		}

		public virtual Com.Zhy.Adapter.Abslistview.ViewHolder SetProgress(int viewId, int
			 progress)
		{
			Android.Widget.ProgressBar view = GetView<ProgressBar>(viewId);
			view.Progress = (progress);
			return this;
		}

		public virtual Com.Zhy.Adapter.Abslistview.ViewHolder SetProgress(int viewId, int
			 progress, int max)
		{
			Android.Widget.ProgressBar view = GetView<ProgressBar>(viewId);
			view.Max = (max);
			view.Progress = (progress);
			return this;
		}

		public virtual Com.Zhy.Adapter.Abslistview.ViewHolder SetMax(int viewId, int max)
		{
			Android.Widget.ProgressBar view = GetView<ProgressBar>(viewId);
			view.Max=(max);
			return this;
		}

		public virtual Com.Zhy.Adapter.Abslistview.ViewHolder SetRating(int viewId, float
			 rating)
		{
			Android.Widget.RatingBar view = GetView<RatingBar>(viewId);
			view.Rating=(rating);
			return this;
		}

		public virtual Com.Zhy.Adapter.Abslistview.ViewHolder SetRating(int viewId, float
			 rating, int max)
		{
			Android.Widget.RatingBar view = GetView<RatingBar>(viewId);
			view.Max = (max);
			view.Rating = (rating);
			return this;
		}

		public virtual Com.Zhy.Adapter.Abslistview.ViewHolder SetTag(int viewId, Java.Lang.Object tag)
		{
			Android.Views.View view = GetView<View>(viewId);
			view.Tag = (tag);
			return this;
		}

		public virtual Com.Zhy.Adapter.Abslistview.ViewHolder SetTag(int viewId, int key, Java.Lang.Object tag)
		{
			Android.Views.View view = GetView<View>(viewId);
			view.SetTag(key,tag);
			return this;
		}

		public virtual Com.Zhy.Adapter.Abslistview.ViewHolder SetChecked(int viewId, bool
			 @checked)
		{
			Android.Widget.ICheckable view = (Android.Widget.ICheckable)GetView<View>(viewId);
			view.Checked = (@checked);
			return this;
		}

		/// <summary>关于事件的</summary>
		public virtual Com.Zhy.Adapter.Abslistview.ViewHolder SetOnClickListener(int viewId
			, Android.Views.View.IOnClickListener listener)
		{
			Android.Views.View view = GetView<View>(viewId);
			view.SetOnClickListener(listener);
			return this;
		}

		public virtual Com.Zhy.Adapter.Abslistview.ViewHolder SetOnTouchListener(int viewId
			, View.IOnTouchListener listener)
		{
			Android.Views.View view = GetView<View>(viewId);
			view.SetOnTouchListener(listener);
			return this;
		}

		public virtual Com.Zhy.Adapter.Abslistview.ViewHolder SetOnLongClickListener(int 
			viewId, Android.Views.View.IOnLongClickListener listener)
		{
			Android.Views.View view = GetView<View>(viewId);
			view.SetOnLongClickListener(listener);
			return this;
		}
	}
}
