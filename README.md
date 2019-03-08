# ContinuousImprovement

https://stackoverflow.com/questions/30690186/how-do-i-access-the-kubernetes-api-from-within-a-pod-container
https://kubernetes.io/docs/tasks/administer-cluster/access-cluster-api/

https://kubernetes.io/docs/concepts/workloads/controllers/jobs-run-to-completion/

## Data files

The files in the `Source/Core/Data/` directory should only contain default data. Unfortunately, adding files to a `.gitignore` file does not tell Git to ignore them, since they are already tracked. A work-around is to run the following command:

```bash
git ls-files -z Source/Core/Data | xargs -0 git update-index --skip-worktree
```

This tells Git to ignore any future changes to all these files. If you add new ones, simply re-run the command.

## GitHub application setup

The Core project requires credentials to the GitHub application to start. So - for development - add the following lines in your shell startup script:

```bash
export GITHUB_APP_ID="..."
export GITHUB_PRIVATE_KEY_PATH="path-to-private-key.pem"
export GITHUB_HOOK_SECRET="..."
export GITHUB_OAUTH_ID="..."
export GITHUB_OAUTH_SECRET="..."
export GITHUB_CI_SMEE_URL="https://smee.io/..."
```

You can get the values of these variables and the private key from the GitHub application setup.

> __NOTE:__ There are two versions of the Continuous Improvement GitHub application:
> 1. ["Continuous Improvement"](https://github.com/apps/continuous-improvement) with callback and webhook URLs that point to Dolittle Studio.
> 2. ["Continuous Improvement - DEV"](https://github.com/apps/continuous-improvement-dev) with callbacks to _localhost_ and webhooks a [smee.io](https://smee.io) channel.
>
> They have their own secrets, ids and keys, so make sure they match for the developent / production setup.

### Running the application with the GitHub integration in development

Once all the environmental variables above have been configured, run - in three separate terminals:
```console
sh:Source/Core $ dotnet run
```
```console
sh:Source/Web $ yarn start
```
```console
sh:* $ smee -u $GITHUB_CI_SMEE_URL -p 5000 -P /thirdparty/github/webhooks/
```

The redirects from the ["Continuous Improvement - DEV"](https://github.com/apps/continuous-improvement-dev) application will then send you to your local development version, and the webhooks will be delivered through the smee.io channel also to the development backend. To delete the installation, follow the link to the application above, and click configure in the top right corner, and then uninstall at the very bottom.