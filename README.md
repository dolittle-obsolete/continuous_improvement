# ContinuousImprovement

https://stackoverflow.com/questions/30690186/how-do-i-access-the-kubernetes-api-from-within-a-pod-container
https://kubernetes.io/docs/tasks/administer-cluster/access-cluster-api/

https://kubernetes.io/docs/concepts/workloads/controllers/jobs-run-to-completion/

## GitHub application setup
The Core project requires credentials to the GitHub application to start. So - for development - add the following lines in your shell startup script:
```bash
export GITHUB_APP_ID="..."
export GITHUB_PRIVATE_KEY_PATH="path-to-private-key.pem"
export GITHUB_HOOK_SECRET="..."
export GITHUB_OAUTH_ID="..."
export GITHUB_OAUTH_SECRET="..."
```
You can get the values of these variables and the private key from the GitHub application setup.