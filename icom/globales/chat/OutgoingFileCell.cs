using System;
using UIKit;
using Foundation;
using CoreGraphics;
namespace icom
{
	[Register("OutgoingFileCell")]
	public partial class OutgoingFileCell : BubbleCell
	{
		static readonly UIImage normalBubbleImage;
		static readonly UIImage highlightedBubbleImage;



		public static readonly NSString CellId = new NSString("OutgoingFile");

		static OutgoingFileCell()
		{
			UIImage mask = UIImage.FromFile("bubble_outgoing.png");

			var cap = new UIEdgeInsets
			{
				Top = 17f,
				Left = 21f,
				Bottom = 17f,
				Right = 26f
			};

			var normalColor = UIColor.FromRGB(105, 178, 174);
			var highlightedColor = UIColor.FromRGB(125, 212, 207);

			normalBubbleImage = CreateColoredImage(normalColor, mask).CreateResizableImage(cap);
			highlightedBubbleImage = CreateColoredImage(highlightedColor, mask).CreateResizableImage(cap);
		}

		public OutgoingFileCell(IntPtr handle)
			: base(handle)
		{
			Initialize();
		}

		public OutgoingFileCell()
		{
			Initialize();
		}

		[Export("initWithStyle:reuseIdentifier:")]
		public OutgoingFileCell(UITableViewCellStyle style, string reuseIdentifier)
			: base(style, reuseIdentifier)
		{
			

			Initialize();
		}

		void Initialize()
		{
			
			BubbleHighlightedImage = highlightedBubbleImage;
			BubbleImage = normalBubbleImage;

			ContentView.AddConstraints(NSLayoutConstraint.FromVisualFormat("H:[bubble]|",
				(NSLayoutFormatOptions)0,
				"bubble", BubbleImageView));
			ContentView.AddConstraints(NSLayoutConstraint.FromVisualFormat("V:|-2-[bubble]-2-|",
				(NSLayoutFormatOptions)0,
				"bubble", BubbleImageView
			));
			BubbleImageView.AddConstraints(NSLayoutConstraint.FromVisualFormat("H:[bubble(>=48)]",
				(NSLayoutFormatOptions)0,
				"bubble", BubbleImageView
			));


			var vSpaceTop = NSLayoutConstraint.Create(MessageLabel, NSLayoutAttribute.Top, NSLayoutRelation.Equal, BubbleImageView, NSLayoutAttribute.Top, 1, 10);
			ContentView.AddConstraint(vSpaceTop);

			var vSpaceBottom = NSLayoutConstraint.Create(MessageLabel, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal, BubbleImageView, NSLayoutAttribute.Bottom, 1, -10);
			ContentView.AddConstraint(vSpaceBottom);

			var msgTrailing = NSLayoutConstraint.Create(MessageLabel, NSLayoutAttribute.Trailing, NSLayoutRelation.LessThanOrEqual, BubbleImageView, NSLayoutAttribute.Trailing, 1, -16);
			ContentView.AddConstraint(msgTrailing);

			var msgCenter = NSLayoutConstraint.Create(MessageLabel, NSLayoutAttribute.CenterX, NSLayoutRelation.Equal, BubbleImageView, NSLayoutAttribute.CenterX, 1, -3);
			ContentView.AddConstraint(msgCenter);
			MessageLabel.TextColor = UIColor.White;



		}
	}
}

