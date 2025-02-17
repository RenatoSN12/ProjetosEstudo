import { useState } from 'react'

function Form() {

    function cadastrarUsuario(e) {
        e.preventDefault()
        console.log("Cadastrou o usuário.")
    }

    const [name, setName] = useState()
    const [password, setPassword] = useState()

    return (
        <div>
            <h1>Meu cadastro:</h1>
            <form onSubmit={cadastrarUsuario}>
                <div>
                    <label htmlFor="name">Nome:</label>
                    <input 
                        name="name" 
                        id="name" 
                        type="text" 
                        placeholder="Digite o seu nome"
                        onChange={(e) => setName(e.target.value)} />
                </div>
                <div>
                    <label htmlFor="passworrd">Senha:</label>
                    <input 
                        name="password" 
                        id="password" 
                        type="password" 
                        placeholder="Digite a sua senha"
                        onChange={(e)=> setPassword(e.target.value)} />
                </div>
                <div>
                    <input type="submit" value="Cadastrar" />
                </div>
            </form>
        </div>
    )
}

export default Form