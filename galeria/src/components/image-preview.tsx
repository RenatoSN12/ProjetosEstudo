import type React from 'react'
import { tv } from 'tailwind-variants'

export const ImagePreviewVariants = tv({
    base: `
      rounded-lg overflow-hidden
    `
})

export const ImagePreviewImageVariants = tv({
    base: `
      w-full h-full object-cover
    `
})

interface ImagePreviewProps extends React.ComponentProps<"img"> {
  imageClassName?: string;
}

export default function ImagePreview({
    className,
    imageClassName,
    ...props
}: ImagePreviewProps) {
    return <div className={ImagePreviewVariants({className})}>
        <img className={ImagePreviewImageVariants({className: imageClassName})} {...props}></img>
    </div>
}