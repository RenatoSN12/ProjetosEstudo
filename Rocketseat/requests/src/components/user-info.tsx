import { useEffect } from "react";
import useUser from "../hooks/use-user";

export default function UserInfo() {
  const { user, userRequestStatus, getUser } = useUser();

  useEffect(() => {
    getUser("1");
  }, [getUser]);

  if (userRequestStatus === "loading") {
    return <div>Carregando usuário...</div>;
  }

  return (
    <ul>
      <li>Id: {user?.id}</li>
      <li>Nome: {user?.name}</li>
    </ul>
  );
}
