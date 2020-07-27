# VeterinarySurgeonApi

Just run 'docker-compose up' from root directory.


By default api runs on port 5000, you can change it in docker-compose.yml file

Also I didn't have a time to make correct retry ([Polly](https://github.com/App-vNext/Polly) library is best candidate for this), system will start after few attempts (just did "restart:always" in docker-compose), because mysql takes some time to become healhty.
