pipeline {
    agent any
    environment {
        DOCKER_COMPOSE_FILE = 'docker-compose.yml'
        DOCKER_HUB_USERNAME = 'tomcorleone'
        ALLOWED_DEPLOY_SERVICES = getServiceList()
    }
    stages {       
        stage('Checkout clone or update repo') {
            steps {
                script {
                    git branch: 'lp/241118_jenkins_test', url: 'https://github.com/ShoppingAllyShop/EllyShop.git'
                }
            }
        }
        stage('Detect Changed Services') {
            steps {
                script {
                    def changedFiles = sh(
                        script: "git diff --name-only HEAD~1 HEAD",
                        returnStdout: true
                    ).trim()

                    echo "Changed files: ${changedFiles}"

                    // Tách rootpath
                    if (changedFiles) {
                        def rootPaths = changedFiles
                            .split('\n')                          // Chia từng dòng
                            .collect { it.split('/')[0].toLowerCase() }         // Lấy phần rootpath (trước `/source`)
                            .unique()                            // Loại bỏ trùng lặp

                        echo "Unique root paths: ${rootPaths}"  

                        // Gán vào biến môi trường nếu cần dùng tiếp
                        env.CHANGED_SERVICES = rootPaths.join(' ')
                    } else {
                        echo "No changes detected."
                        env.CHANGED_SERVICES = ''
                    }
                }
            }
        }
        // stage('Login to Docker') {
        //     steps {
        //         withCredentials([string(credentialsId: 'elly_dockerhub_token', variable: 'DOCKER_HUB_TOKEN')]) {
        //             sh '''
        //                 echo $DOCKER_HUB_TOKEN | docker login -u $DOCKER_HUB_USERNAME --password-stdin
        //             '''
        //         }
        //     }
        // }
        stage('Build and push image to registry') {           
            when {
                expression { env.CHANGED_SERVICES != '' }
            }
            steps {
                script {                
                        // Lặp qua các service thay đổi và thực hiện build + deploy
                        echo "Start build"
                        echo "CHANGED_SERVICES: ${env.CHANGED_SERVICES}"
                        env.CHANGED_SERVICES.split(' ').each { service ->
                            echo "Building and Deploying ${service}"
                            echo "aaaaaaaaaa: ${ALLOWED_DEPLOY_SERVICES.contains('jiji')}"
                            if (ALLOWED_DEPLOY_SERVICES.contains(service) == false){
                                echo "skip service ${service}"
                                return
                            }
                            // Create docker tag
                            def dockerImageTag = "tomcorleone/elly-mayo-${service}:latest"
                            
                            // Build Docker image
                            sh """
                                docker-compose -f ${DOCKER_COMPOSE_FILE} build ${service}
                                docker tag ${service} ${dockerImageTag}
                            """

                            // Push Docker image lên Docker Hub
                            echo "Push image: ${TAG_NAME_IMAGE_FRONTEND}"
                            try {
                                sh "docker push ${dockerImageTag}"

                                echo "Push successful"
                    
                                // Remove Docker images after push
                                sh """
                                    docker rmi ${TAG_NAME_IMAGE_FRONTEND} || echo "Image ${TAG_NAME_IMAGE_FRONTEND} already removed"
                                    docker rmi ${dockerImageTag} || echo "Image ${dockerImageTag} already removed"
                                """
                                echo "Repmove successfulfly. Image: ${service}"
                            } catch (e) {
                                error "Push to dockerhub failed: ${e}"
                            }                        
                         }
                        }
                    }
        }
        stage('Deploy server'){
            steps {
                withCredentials([sshUserPrivateKey(credentialsId: 'Elly_SSH_phantanloc', keyFileVariable: 'PRIVATE_KEY')]) {
                    sh '''
                        cp $PRIVATE_KEY /tmp/temp_key
                        chmod 600 /tmp/temp_key
                        ls -l /tmp/temp_key
                        stat -c "%a %n" /tmp/temp_key
                        ssh -o StrictHostKeyChecking=no -i /tmp/temp_key phantanloc@14.225.254.235
                    '''
                    script {
                        env.CHANGED_SERVICES.split(' ').each { service ->
                            echo "Building and Deploying ${service}"
                            if (ALLOWED_DEPLOY_SERVICES.contains(service) == false){
                                echo "skip service ${service}"
                                return
                            }
                            // Tạo tag với ngày giờ
                            def dockerImageTag = "tomcorleone/elly-mayo-${service}:latest"
                            def imageName = "elly_${service}"
                            def port = selectPort(service)
                            echo "Start pull and run image"
                            echo "dockerImageTag: ${dockerImageTag}. imageName: ${imageName}. port: ${port}"

                            sh """
                                ssh -o StrictHostKeyChecking=no -i /tmp/temp_key phantanloc@14.225.254.235 '
                                    docker pull ${dockerImageTag} &&
                                    docker stop ${imageName} || true &&
                                    docker rm ${imageName} || true &&
                                    docker run -d --name ${imageName} -p ${port}:80 ${dockerImageTag}
                                '
                            """          
                        }
                    }
                    sh 'rm -f /tmp/temp_key' // Clean up key            
                }
            }
        }
    }
    post {
        always {
            echo "Pipeline completed."
        }
    }
}

// define port for services
def selectPort(serviceName) {
    switch (serviceName) {
        case 'frontend':
            return '3000'
            break
        case 'gateway':
            return '7000'
            break
        case 'user.api':
            return '7135'
            break
        case 'catalog':
            return '7021'
            break
        default:
            echo 'Select port error!!'
            break
    }
}

def getServiceList(){
  return ['Item1', 'Item2', 'Item3']
}
