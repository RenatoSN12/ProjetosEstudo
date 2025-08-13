import type React from 'react'
import { tv } from 'tailwind-variants'

export const ImageFilePreviewVariants = tv({
    base: `
      rounded-lg overflow-hidden
    `
})

export const FilePreviewImageVariants = tv({
    base: `
      w-full h-full object-cover
    `
})

interface ImageFilePreviewProps extends React.ComponentProps<"img"> {
  imageClassName?: string;
}

export default function ImageFilePreview({
    className,
    imageClassName,
    ...props
}: ImageFilePreviewProps) {
    return <div className={ImageFilePreviewVariants({className})}>
        <img className={FilePreviewImageVariants({className: imageClassName})} {...props}></img>
    </div>
}