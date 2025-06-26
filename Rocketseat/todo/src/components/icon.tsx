import { cva, type VariantProps } from "class-variance-authority";

interface IconProps
  extends React.ComponentProps<"svg">,
    VariantProps<typeof iconVariants> {
  svg: React.FC<React.ComponentProps<"svg">>;
}

// eslint-disable-next-line react-refresh/only-export-components
export const iconVariants = cva("", {
  variants: {
    animate: {
      false: "",
      true: "animate-spin",
    },
  },
  defaultVariants: {
    animate: false,
  },
});

export default function Icon({
  svg: SvgComponent,
  animate,
  className,
  ...props
}: IconProps) {
  return (
    <SvgComponent
      className={iconVariants({ animate, className })}
      {...props}
    ></SvgComponent>
  );
}
