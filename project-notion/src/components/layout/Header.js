import { useState } from "react";
import { IoIosMenu } from "react-icons/io";
import logo from "../../img/logo.png";
import styles from "./Header.module.css";

function Header() {
  const [isMenuOpen, setIsMenuOpen] = useState(false);

  const toggleMenu = () => {
    setIsMenuOpen(!isMenuOpen);
  };

  return (
    <header className={styles.header}>
      <div className={`${styles.logo} ${isMenuOpen ? styles.logoHidden : ""}`}>
        <img src={logo} alt="logo-notion"/>
        <h1>Protótio Notion</h1>
      </div>
    </header>
  );
}

export default Header;