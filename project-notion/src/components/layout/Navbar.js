import styles from './Navbar.module.css';

function Navbar({ isMenuOpen }) {
  return (
    <nav className={`${styles.nav} ${isMenuOpen ? styles.navOpen : ""}`}>
      <ul className={styles.navList}>
        <li className={styles.navItem}>
          <a href="/" className={styles.navLink}>
            Home
          </a>
        </li>
        <li className={styles.navItem}>
          <a href="/about" className={styles.navLink}>
            Sobre
          </a>
        </li>
        <li className={styles.navItem}>
          <a href="/services" className={styles.navLink}>
            Serviços
          </a>
        </li>
        <li className={styles.navItem}>
          <a href="/contact" className={styles.navLink}>
            Contato
          </a>
        </li>
      </ul>
    </nav>
  );
}

export default Navbar;