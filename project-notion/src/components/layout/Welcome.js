import styles from './Welcome.module.css'

function Welcome() {
    return (
        <section className={styles.Welcome}>
            <div className={styles.Info}>
                <h1>Bem-vindo ao <span>Controle Financeiro</span></h1>
                <p><span>Gerencie</span> suas finanças com <span>facilidade</span> e <span>eficiência</span>.
                    Tenha tudo organizado <span>num só lugar</span>, com a ferramenta mais <span>simples</span>
                    e <span>prática</span> que você vai encontrar.</p>
                <button>Comece agora!</button>
            </div>
        </section>
    )
}

export default Welcome