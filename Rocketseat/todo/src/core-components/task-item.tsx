import Card from "../components/card";
import InputCheckbox from "../components/input-checkbox";
import Text from "../components/text";
import TrashIcon from "../assets/icons/trash.svg?react";
import PencilIcon from "../assets/icons/pencil.svg?react";
import XIcon from "../assets/icons/x.svg?react";
import CheckIcon from "../assets/icons/check.svg?react";
import ButtonIcon from "../components/button-icon";
import { useState } from "react";
import InputText from "../components/input-text";

export default function TaskItem() {
  const [isEditing, setIsEditing] = useState(false);

  function handleEditTask() {
    setIsEditing(true);
  }

  function cancelEditTask() {
    setIsEditing(false);
  }

  return (
    <Card size="md" className="flex items-center gap-4">
      {!isEditing ? (
        <>
          <InputCheckbox />
          <Text className="flex-1">🛒 Fazer as compras</Text>
          <div className="flex gap-1">
            <ButtonIcon icon={TrashIcon} variant="tertiary"></ButtonIcon>
            <ButtonIcon
              icon={PencilIcon}
              variant="tertiary"
              onClick={handleEditTask}
            ></ButtonIcon>
          </div>
        </>
      ) : (
        <>
          <InputText className="flex-1" />
          <div className="flex gap-1">
            <ButtonIcon
              icon={XIcon}
              variant="secondary"
              onClick={cancelEditTask}
            ></ButtonIcon>
            <ButtonIcon icon={CheckIcon} variant="primary"></ButtonIcon>
          </div>
        </>
      )}
    </Card>
  );
}
