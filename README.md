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
export GITHUB_INSTALLATION_TENANT_MAP_PATH="path-to-existing-folder-where-tenant-map-file-will-be-created"
```
You can get the values of these variables and the private key from the GitHub application setup.

> __NOTE:__ There are two versions of the Continuous Improvement GitHub application:
> 1. ["Continuous Improvement"](https://github.com/apps/continuous-improvement) with callback and webhook URLs that point to Dolittle Studio.
> 2. ["Continuous Improvement - DEV"](https://github.com/apps/continuous-improvement-dev) with callbacks to _localhost_ and webhooks a [smee.io](https://smee.io) channel.
>
> They have their own secrets, ids and keys, so make sure they match for the developent / production setup.