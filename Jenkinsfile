// define port for services
def selectPort(serviceName) {
    switch (serviceName) {
        case 'frontend':
            return '3000'
            break
        case 'gateway':
            return '7000'
            break
        case 'user':
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

pipeline {
    agent any
    environment {
        DOCKER_COMPOSE_FILE = 'docker-compose.yml'
        DOCKER_HUB_USERNAME = 'tomcorleone'
        TAG_NAME_IMAGE_FRONTEND = 'elly-frontend'
        SSH_KEY = credentials('elly_ssh_ubuntu')
    }
    stages {
        stage('Checkout clone or update repo') {
            steps {
                script {
                    git branch: 'lp/241118_jenkins_test', url: 'https://github.com/ShoppingAllyShop/EllyShop.git'
                }
            }
        }
        // stage('Detect Changed Services') {
        //     steps {
        //         script {
        //             // Sử dụng git diff để tìm các thư mục service thay đổi
        //             // Lấy danh sách file thay đổi (ví dụ giả định ở đây)
        //             // sh "git branch"
        //             // sh "git status"
        //             // sh "git fetch origin lp/241118_jenkins_test"
        //             def changedFiles = sh(
        //                 script: "git diff --name-only HEAD~1 HEAD",
        //                 //script: "git --no-pager diff origin/lp/241118_jenkins_test --name-only",
        //                 returnStdout: true
        //             ).trim()

        //             echo "Changed files: ${changedFiles}"

        //             // Tách rootpath
        //             if (changedFiles) {
        //                 def rootPaths = changedFiles
        //                     .split('\n')                          // Chia từng dòng
        //                     .collect { it.split('/')[0].toLowerCase() }         // Lấy phần rootpath (trước `/source`)
        //                     .unique()                            // Loại bỏ trùng lặp

        //                 echo "Unique root paths: ${rootPaths}"  

        //                 // Gán vào biến môi trường nếu cần dùng tiếp
        //                 env.CHANGED_SERVICES = rootPaths.join(' ')
        //             } else {
        //                 echo "No changes detected."
        //                 env.CHANGED_SERVICES = ''
        //             }
        //         }
        //     }
        // }
        // stage('Login to Docker') {
        //     steps {
        //         withCredentials([string(credentialsId: 'elly_dockerhub_token', variable: 'DOCKER_HUB_TOKEN')]) {
        //             sh '''
        //             echo $DOCKER_HUB_TOKEN | docker login -u $DOCKER_HUB_USERNAME --password-stdin
        //             '''
        //         }
        //     }
        // }
        // stage('Build') {           
        //     when {
        //         expression { env.CHANGED_SERVICES != '' }
        //     }
        //     steps {
        //         script {                
        //                 // Lặp qua các service thay đổi và thực hiện build + deploy
        //                 echo "Start build"
        //                 echo "CHANGED_SERVICES: ${env.CHANGED_SERVICES}"
        //                 env.CHANGED_SERVICES.split(' ').each { service ->
        //                 echo "Building and Deploying ${service}"
        //                 if (service != "frontend"){
        //                     echo "skip service ${service}"
        //                     return
        //                 }
        //                 // Tạo tag với ngày giờ
        //                 def dockerImageTag = "tomcorleone/elly-mayo-${service}:latest"
                        
        //                 // Build Docker image
        //                 sh """
        //                 docker-compose -f ${DOCKER_COMPOSE_FILE} build ${service}
        //                 docker tag ${TAG_NAME_IMAGE_FRONTEND} ${dockerImageTag}
        //                 """

        //                  // Push Docker image lên Docker Hub
        //                 echo "Push image: ${TAG_NAME_IMAGE_FRONTEND}"
        //                 try {
        //                     sh "docker push ${dockerImageTag}"
        //                 } catch (e) {
        //                     error "Push to dockerhub failed: ${e}"
        //                 }

        //                 //clean image
        //                 // echo "Clear image: ${service}"
        //                 // sh "docker image rm...."

        //                 // Deploy (ví dụ: chỉ start container thay đổi)
        //                 // sh """
        //                 // docker-compose -f ${DOCKER_COMPOSE_FILE} up -d ${service}
        //                 // """                        
        //             }
        //         }
        //     }
        // }
        stage('Deploy server'){
            steps {
                // script {
                //     // sh """
                //     // ssh -i $SSH_KEY phantanloc@14.225.254.255 touch ptl.txt'
                //     // """
                //      sh 'ls -l $SSH_KEY'
                //      sh 'chmod 600 $SSH_KEY'
                //      sh 'ls -l $SSH_KEY'
                //     //  sh('ssh -o StrictHostKeyChecking=no -i $SSH_KEY phantanloc@14.225.254.235 touch ptl.txt')

                //      sh "chmod 600 ${SSH_KEY}"

                //     // Kết nối SSH và thực hiện lệnh
                //     sh """
                //     ssh -o StrictHostKeyChecking=no -i ${SSH_KEY} phantanloc@14.225.254.235 touch ptl.txt
                //     """
                // }
                script {
                    sh """
                    echo "${SSH_KEY}" > /tmp/jenkins_ssh_key
                    chmod 600 /tmp/jenkins_ssh_key
                    cat /tmp/jenkins_ssh_key
                    """

                    // Thực thi lệnh SSH sử dụng key đã sửa quyền
                    sh """
                    ssh -o StrictHostKeyChecking=no -i /tmp/jenkins_ssh_key phantanloc@14.225.254.235 touch ptl.txt
                    """

                    // Xóa file tạm sau khi sử dụng (để bảo mật)
                    sh "rm -f /tmp/jenkins_ssh_key"
                }
            }
        }
        //  stage('Deploy server'){
        //     steps{
        //        script{
        //         sshagent(credentials: ['elly_ssh_ubuntu']) {
        //             // sh 'chmod -R 600 /var/jenkins_home/workspace/EllyShop@tmp'
        //             sh 'ssh -o StrictHostKeyChecking=no -l phantanloc 14.225.254.235'

        //             env.CHANGED_SERVICES.split(' ').each { service ->
        //                 echo "Building and Deploying ${service}"
        //                 if (service != "frontend"){
        //                     echo "skip service ${service}"
        //                     return
        //                 }
        //                 // Tạo tag với ngày giờ
        //                 def dockerImageTag = "tomcorleone/elly-mayo-${service}:latest"
        //                 def imageName = "elly_${service}"
        //                 def port = selectPort(service)
        //                 echo "Start pull and run image"
        //                 echo "dockerImageTag: ${dockerImageTag}. imageName: ${imageName}. port: ${dockerImageTag}"

        //             sh  """
        //                 # Kéo Docker image từ Docker Hub
        //                 docker pull ${dockerImageTag}

        //                 # Dừng và xóa container cũ nếu có
        //                 docker stop ${imageName} || true
        //                 docker rm ${imageName} || true

        //                 # Chạy container mới
        //                 docker run -d --name ${imageName} -p ${port}:80 ${dockerImageTag}
        //                 """                
        //             }                   
        //         }
        //        } 
        //     }
        // }
    }
    post {
        always {
            echo "Pipeline completed."
        }
    }
}