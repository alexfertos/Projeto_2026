document.addEventListener('DOMContentLoaded', function(){
  const banner = document.getElementById('banner')
  const topo = document.getElementById("topo")

  setTimeout(()=>{
    banner.style.opacity = '1'
  }, 200)
  setTimeout(()=>{
    topo.style.opacity = '1'
  }, 400)
})
function calcularIMC(){
        let altura = document.querySelector("#altura").value
        let peso = document.querySelector("#peso").value

        altura = parseInt(altura)
        peso = parseFloat(peso)
        altura = altura/100
        console.log(peso) 
        console.log(altura)

        imc = peso/(altura*altura)
        console.log(imc.toFixed(2))

        var resultado = ""

        if(imc < 18.5){
          resultado = 'Baixo peso'
          console.log("Baixo peso")
        } else if(imc >= 18.5 && imc <=24.9){
          resultado = 'Peso Normal'
          console.log("Peso normal")
        } else if(imc >= 25 && imc <=29.9){
          resultado = 'Sobrepeso'
          console.log("sobrepeso")
        } else if(imc >= 30 && imc <=34.9){
          resultado = 'Obesidade Grau I'
          console.log("Obesidade Grau I")
        } else if(imc >= 35 && imc <=39.9){
          resultado = 'Obesidade Grau II'
          console.log("Obesidade Grau II")
        } else if(imc >= 40){
          resultado = 'Obesidade Grau III (mórbida)'
          console.log("Obesidade Grau III (mórbida)")
        }
        console.log("Cade" + resultado)
        
        const resultadoLabel = document.getElementById("resultado-label")

        resultadoLabel.value = "Resultado: " + resultado

        const inputIMC = document.getElementById("input-imc")

        inputIMC.value = "Seu IMC é: " + imc.toFixed(2)  

        var pesoIdealMinimo = 18.5 *(altura*altura)
        var pesoIdealMaximo = 24.9 *(altura*altura)

        console.log(pesoIdealMinimo.toFixed(2))
        console.log(pesoIdealMaximo.toFixed(2))

        const pesoIdeal = document.getElementById("peso-ideal")

        pesoIdeal.value = "Peso ideal: Entre " + pesoIdealMinimo.toFixed(1) + "kg á " + pesoIdealMaximo.toFixed(1) + "kg."

        const classeFormImc = document.getElementsByClassName("form-imc")

        classeFormImc.add(".fechar")
      }

      function calcularAgua(){
        const peso = document.getElementById("peso-agua").value
        const qtaAgua = document.getElementById("input-litros")

        var quantidade = peso * (35 /1000)
        console.log(quantidade)
        qtaAgua.value = quantidade + " Litros de água por dia"
      }

      function calcularTMB(){
        const peso = document.getElementById("peso-TMB").value
        const altura = document.getElementById("altura-TMB").value
        const idade = document.getElementById("idade-TMB").value
        const genero = document.getElementById("genero").value
        const atividade = document.getElementById("atividade").value

        console.log(peso)
        console.log(altura)
        console.log(idade)
        console.log(genero)
        console.log(atividade)

        if(genero === "M"){
          var TMB = 88.36 + (13.4 * peso) + (4.8 * altura) - (5.7 * idade)
          var resultadoFinal = ""
          console.log(TMB)
          if(atividade === "1"){
            var resultadoFinal = TMB * 1.2
          } else if(atividade === "2"){
            var resultadoFinal = TMB * 1.375
          } else if(atividade === "3"){
            var resultadoFinal = TMB * 1.55
          } else if(atividade === "4"){
            var resultadoFinal = TMB * 1.725
          } else if(atividade === "5"){
            var resultadoFinal = TMB * 1.9
          }
          console.log(resultadoFinal)
          const input = document.getElementById("input-tmb")
          input.value = "Gasto calórico: " + resultadoFinal.toFixed(2) + " kcal/dia"
        } else if(genero === "F"){
          var TMBF = 447.6 + (9.2 * peso) + (3.1 * altura) - (4.3 * idade)
          var resultadoFinal = ""
          console.log(TMBF)
          if(atividade === "1"){
            var resultadoFinal = TMBF * 1.2
          } else if(atividade === "2"){
            var resultadoFinal = TMBF * 1.375
          } else if(atividade === "3"){
            var resultadoFinal = TMBF * 1.55
          } else if(atividade === "4"){
            var resultadoFinal = TMBF * 1.725
          } else if(atividade === "5"){
            var resultadoFinal = TMBF * 1.9
          }
          console.log(resultadoFinal)
          const input = document.getElementById("input-tmb")
          input.value = "Gasto calórico: " + resultadoFinal.toFixed(2) + " kcal/dia"
        }


      }

      function login(){
        const login = document.getElementById('loginModal')

        login.classList.toggle('abrir')
      }

      window.addEventListener('click', e =>{
        const botaoLogin = document.getElementById('botaoLogin')
        const login = document.getElementById('loginModal') 

        if(!botaoLogin.contains(e.target) && !login.contains(e.target)){
          if(login.classList.contains('abrir')){
            login.classList.remove('abrir')
          }
        }
      }
    )

    document.addEventListener('keydown', function(e){
      const login = document.getElementById('loginModal')

      if(login.classList.contains('abrir')){
        login.classList.remove('abrir')
      }
    })