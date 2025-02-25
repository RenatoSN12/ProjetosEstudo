import styles from './Welcome.module.css'
import Header from '../layout/Header'

function Welcome() {
    return (
        <section className={styles.Welcome}>
        <Header/>
            <div className={styles.Info}>
                <h1>Bem-vindo ao <span>Controle Financeiro</span></h1>
                <p><span>Gerencie</span> suas finanças com <span>facilidade</span> e <span>eficiência</span>.
                    Tenha tudo organizado <span>num só lugar</span>, com a ferramenta mais <span>simples </span>
                    e <span>prática</span> que você vai encontrar.</p>
                <button>Comece agora!</button>
            </div>
        </section>
    )
}

export default Welcome