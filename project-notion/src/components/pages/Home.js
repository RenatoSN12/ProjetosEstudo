import Welcome from "../layout/Welcome"
import Advantage from "../layout/Advantage"
import FirstImage from '../../img/first-advantage.jpg'
import SecondImage from '../../img/second-advantage.png'
import ThirdImage from '../../img/third-advantage.png'

import styles from './Home.module.css'

function Home() {

    return (
        <main className={styles.Home}>
            <Welcome/>
            <Advantage 
                title="Organização simplificada"
                text="Tenha todas as suas finanças em um único lugar, facilitando o
                    acompanhamento de gastos, receitas e investimentos de forma clara e
                    organizada."
                image={FirstImage}
            />
            <Advantage 
                title="Decisões financeiras mais inteligentes"
                text="Acesse relatórios e gráficos detalhados que ajudam a entender seus
                    hábitos de gastos e planejar um futuro financeiro mais seguro."
                image={SecondImage}
                inverse={true}
            />
            <Advantage 
                title="Acesso conveniente em qualquer lugar"
                text="Gerencie suas finanças de qualquer dispositivo, a qualquer momento,
                    com sincronização em tempo real e suporte offline quando necessário."
                image={ThirdImage}
            />
        </main>
    )
}

export default Home