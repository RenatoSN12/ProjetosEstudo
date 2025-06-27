import { cva, cx, type VariantProps } from "class-variance-authority";
import Text from "./text";
import Skeleton from "./skeleton";

interface BadgeProps
  extends React.ComponentProps<"div">,
    VariantProps<typeof badgeVariants> {
  loading?: boolean;
}

// eslint-disable-next-line react-refresh/only-export-components
export const badgeTextVariants = cva("", {
  variants: {
    variant: {
      none: "",
      primary: "text-green-dark",
      secondary: "text-pink-dark",
    },
  },
  defaultVariants: {
    variant: "primary",
  },
});

// eslint-disable-next-line react-refresh/only-export-components
export const badgeSkeletonVariants = cva("", {
  variants: {
    size: {
      sm: "w-6 h-6",
    },
  },
  defaultVariants: {
    size: "sm",
  },
});

// eslint-disable-next-line react-refresh/only-export-components
export const badgeVariants = cva(
  "inline-flex items-center justify-center rounded-full",
  {
    variants: {
      variant: {
        none: "",
        primary: "bg-green-light",
        secondary: "bg-pink-light",
      },
      size: {
        sm: "py-0.5 px-2",
      },
    },
    defaultVariants: {
      variant: "primary",
      size: "sm",
    },
  }
);

export default function Badge({
  variant,
  size,
  className,
  children,
  loading,
  ...props
}: BadgeProps) {
  if (loading) {
    return (
      <Skeleton
        rounded="full"
        className={cx(
          badgeVariants({ variant: "none" }),
          badgeSkeletonVariants({ size }),
          className
        )}
      />
    );
  }

  return (
    <div className={badgeVariants({ variant, size, className })} {...props}>
      <Text className={badgeTextVariants({ variant })} variant="body-sm-bold">
        {children}
      </Text>
    </div>
  );
}
