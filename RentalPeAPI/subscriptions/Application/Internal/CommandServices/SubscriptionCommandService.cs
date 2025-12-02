using RentalPeAPI.Shared.Domain.Repositories;
using RentalPeAPI.subscriptions.Domain.Model.Aggregates;
using RentalPeAPI.subscriptions.Domain.Model.Commands;
using RentalPeAPI.subscriptions.Domain.Model.ValueObjects;
using RentalPeAPI.subscriptions.Domain.Repositories;
using RentalPeAPI.subscriptions.Domain.Services;

namespace RentalPeAPI.subscriptions.Application.Internal.CommandServices;

public class SubscriptionCommandService(
    ISubscriptionRepository subscriptionRepository,
    IUnitOfWork unitOfWork) : ISubscriptionCommandService
{
    public async Task<Subscription?> Handle(CreateSubscriptionCommand command)
    {
        
        if (command.EndDate <= command.StartDate)
            return null; 

        try
        {
            var price  = new SubscriptionPrice(command.Price);
            var period = new SubscriptionPeriod(command.StartDate, command.EndDate);

            var subscription = new Subscription(
                customerId: command.CustomerId,
                plan:       command.Plan,
                price:      price,
                period:     period);

            await subscriptionRepository.AddAsync(subscription);
            await unitOfWork.CompleteAsync();
            return subscription;
        }
        catch
        {
            return null;
        }
    }

    public async Task<Subscription?> Handle(CancelSubscriptionCommand command)
    {
        var subscription = await subscriptionRepository.FindByIdAsync(command.SubscriptionId);
        if (subscription is null) return null;

        try
        {
            subscription.Cancel();
            subscriptionRepository.Update(subscription);
            await unitOfWork.CompleteAsync();
            return subscription;
        }
        catch
        {
            return null;
        }
    }

    public async Task<Subscription?> Handle(ExpireSubscriptionCommand command)
    {
        var subscription = await subscriptionRepository.FindByIdAsync(command.SubscriptionId);
        if (subscription is null) return null;

        try
        {
            subscription.Expire();
            subscriptionRepository.Update(subscription);
            await unitOfWork.CompleteAsync();
            return subscription;
        }
        catch
        {
            return null;
        }
    }

    public async Task<Subscription?> Handle(ChangeSubscriptionPlanCommand command)
    {
        var subscription = await subscriptionRepository.FindByIdAsync(command.SubscriptionId);
        if (subscription is null) return null;

        var newPrice = new SubscriptionPrice(command.NewPrice);

        try
        {
            subscription.ChangePlan(command.NewPlan, newPrice);
            subscriptionRepository.Update(subscription);
            await unitOfWork.CompleteAsync();
            return subscription;
        }
        catch
        {
            return null;
        }
    }
}