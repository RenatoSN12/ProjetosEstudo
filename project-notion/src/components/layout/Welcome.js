import WelcomeImage from '../../img/welcome-image.jpg'

import styles from './Welcome.module.css'

function Welcome() {
    return (
        <section className={styles.Welcome}>
            <div className={styles.Info}>
                <h1>Bem-vindo ao Controle Financeiro</h1>
                <p>Gerencie suas finanças com facilidade e eficiência</p>
                <button>Comece agora!</button>
            </div>
            <img src={WelcomeImage} alt="welcome-image"></img>
        </section>
    )
}

export default Welcome