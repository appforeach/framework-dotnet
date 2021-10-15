
namespace FrameworkSagas.Infrastructure
{
    public abstract class BaseLifecycle<TEntity, TStateDataType>
    {
        protected abstract TStateDataType StateBasedOn(TEntity entity);

        protected void Initial(params LifecycleSpecification[] lifecycleSpecifications)
        {

        }

        protected void When(TStateDataType state, params LifecycleSpecification[] lifecycleSpecifications)
        {

        }

        protected LifecycleSpecification Allow<TInput>()
        {
            return null;
        }
    }
}
