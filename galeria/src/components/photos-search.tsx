import InputText from "./input-text";
import SearchIcon from "../assets/icons/search.svg?react"
import React from "react";
import { debounce } from "../helpers/utils";

export default function PhotosSearch() {

    const [inputValue, setInputValue] = React.useState("");

    // const debouncedSetValue = React.useCallback(
    //     debounce((value:string) )
    // )

    // function handleInputChange(e: React.ChangeEvent<HTMLInputElement>){
    //     const value = e.target.value;

        
    // }

    return (
        <InputText
          icon={SearchIcon}
          placeholder="Buscar fotos..."
          className="flex-1"
        />
    )
}