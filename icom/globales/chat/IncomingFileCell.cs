using Foundation;
using UIKit;
using CoreGraphics;
using System;
namespace icom
{
	[Register("IncomingFileCell")]
	public partial class IncomingFileCell : BubbleCell
	{
		static readonly UIImage normalBubbleImage;
		static readonly UIImage highlightedBubbleImage;

		public static readonly NSString CellId = new NSString("IncomingFile");

		static IncomingFileCell()
		{
			UIImage mask = UIImage.FromFile("bubble_regular.png");

			var cap = new UIEdgeInsets
			{
				Top = 17f,
				Left = 26f,
				Bottom = 17f,
				Right = 21f,
			};

			var normalColor = UIColor.FromRGB(105, 178, 174);
			var highlightedColor = UIColor.FromRGB(125, 212, 207);

			normalBubbleImage = CreateColoredImage(normalColor, mask).CreateResizableImage(cap);
			highlightedBubbleImage = CreateColoredImage(highlightedColor, mask).CreateResizableImage(cap);

		}

		public IncomingFileCell(IntPtr handle)
			: base(handle)
		{
			Initialize();
		}

		public IncomingFileCell()
		{
			Initialize();
		}

		[Export("initWithStyle:reuseIdentifier:")]
		public IncomingFileCell(UITableViewCellStyle style, string reuseIdentifier)
			: base(style, reuseIdentifier)
		{
			Initialize();
		}

		void Initialize()
		{			
			BubbleHighlightedImage = highlightedBubbleImage;
			BubbleImage = normalBubbleImage;
				

			ContentView.AddConstraints(NSLayoutConstraint.FromVisualFormat("H:|-37-[bubble]",
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


			ContentView.AddConstraints(NSLayoutConstraint.FromVisualFormat("H:|[usuariolo]",
				(NSLayoutFormatOptions)0,
				"usuariolo", UsuarioLabel));
			ContentView.AddConstraints(NSLayoutConstraint.FromVisualFormat("V:|-40-[usuariolo]-2-|",
				(NSLayoutFormatOptions)0,
				"usuariolo", UsuarioLabel
			));
			





			var vSpaceTop = NSLayoutConstraint.Create(MessageLabel, NSLayoutAttribute.Top, NSLayoutRelation.Equal, BubbleImageView, NSLayoutAttribute.Top, 1, 10);
			ContentView.AddConstraint(vSpaceTop);

			var vSpaceBottom = NSLayoutConstraint.Create(MessageLabel, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal, BubbleImageView, NSLayoutAttribute.Bottom, 1, -10);
			ContentView.AddConstraint(vSpaceBottom);

			var msgLeading = NSLayoutConstraint.Create(MessageLabel, NSLayoutAttribute.Leading, NSLayoutRelation.GreaterThanOrEqual, BubbleImageView, NSLayoutAttribute.Leading, 1, 16);
			ContentView.AddConstraint(msgLeading);

			var msgCenter = NSLayoutConstraint.Create(MessageLabel, NSLayoutAttribute.CenterX, NSLayoutRelation.Equal, BubbleImageView, NSLayoutAttribute.CenterX, 1, 3);
			ContentView.AddConstraint(msgCenter);


		}

	}
}

