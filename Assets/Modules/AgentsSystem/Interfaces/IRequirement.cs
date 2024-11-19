using System.Collections.Generic;
using System.Linq;

namespace AgentsSystem {

    public interface IRequrement {
        
    }
    abstract class GroupRequrement : IRequrement{
        private int requiredCount;
        public abstract bool Evaluate(List<Agent> agents, int requiredCount);
    }

    abstract class SingleAgentRequirement : IRequrement{
        public abstract bool Evaluate(Agent agent);
    }


    class AgentHasTraitRequirement : SingleAgentRequirement {
        private int requiredTrait;
        public AgentHasTraitRequirement(int requiredTrait) {
            this.requiredTrait = requiredTrait;
        }
        public override bool Evaluate(Agent agent) {
            return agent.traits.Exists(trait => trait.id == requiredTrait);
        }
    };

    class GroupHasTraitRequirement : GroupRequrement {
        private int requiredTrait;

        public GroupHasTraitRequirement(int requiredTrait) {
            this.requiredTrait = requiredTrait;
        }
        public override bool Evaluate(List<Agent> agents, int requiredCount) {
            int matchingCount = agents.Count(agent => agent.traits.Exists(trait => trait.id == requiredTrait));
            if (requiredCount == -1) return matchingCount == agents.Count;
            if (requiredCount == 0) return matchingCount > 0;
            return requiredCount == matchingCount;
        }
    };
}

//накидываю мысли, какие могут быть требования:
//Agent has a trait

//Agent has a trait tag

//Agent is of certain age (more, less, or exact)

//Agent has a certain health (more, less, or exact)

//Agent has a certain relationship type (в целом, не важно, с кем - "есть хотя бы один друг", "есть хотя бы один враг")

//Agent has a certain relationship type with a specific agent

//Agent has a certain relationship type with an agent that has a specific trait (то есть не важно кто именно, главное, что какой-то чел дружит с пироманом)

//Agent has a certain relationship type with a specific group of agents (towards every single one of that second group)

//Group of agents - я думаю о ситуациях "у этой группы есть что-то общее" или "все, у кого есть вот это": (все/не все) активные агенты из одной организации имеют ещё и Х черту
//например, все участники "Революционеры-волшебники" имеют черту "героизм" с доп. условием, что участников как минимум 5
//или "есть три разных(!) человека в одном и том же городе, у первого есть черта Х, у второго черта Y, и у всех трёх между собой отношение "Ненависть"
//особенно в последнем случае - я не представляю пока что, как такие проверки будут в принципе проходить.
//понятно, что это комбинация нескольких проверок с сохранением/подбором, каких именно агентов мы сейчас проверяем
//но как это сделать?...

//Group of agents has at least one agent with a trait

//Group of agents has at least one agent with a trait tag

//Group of agents has at least one agent with an age requirement

//Group of agents has at least one agent with a certain relationship type

//Group of agents has at least one agent with a certain relationship type towards a specific agent

//Group of agents has at least one agent with a certain relationship type towards a specific group of agents

//Group of agents all have a trait

//Group of agents all have a trait tag

//..etc