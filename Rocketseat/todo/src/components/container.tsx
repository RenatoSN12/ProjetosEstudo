import { cva, type VariantProps } from "class-variance-authority";
import { createElement } from "react";

// eslint-disable-next-line react-refresh/only-export-components
export const containerVariants = cva("mx-auto", {
  variants: {
    size: {
      md: "max-w-[31.5rem] px-2",
    },
  },
  defaultVariants: {
    size: "md",
  },
});

interface ContainerProps
  extends VariantProps<typeof containerVariants>,
    React.ComponentProps<"div"> {
  as?: keyof React.JSX.IntrinsicElements;
}

export default function Container({
  as = "div",
  size,
  className,
  children,
  ...props
}: ContainerProps) {
  return createElement(
    as,
    {
      className: containerVariants({ size, className }),
      ...props,
    },
    children
  );
}
