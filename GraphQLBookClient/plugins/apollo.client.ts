import { defineNuxtPlugin } from '#app';
import { ApolloClient, createHttpLink, InMemoryCache } from '@apollo/client/core';
import { provideApolloClient } from '@vue/apollo-composable';
import { GraphQLWsLink } from '@apollo/client/link/subscriptions';
import { createClient } from 'graphql-ws';

export default defineNuxtPlugin((nuxtApp) => {
    const httpLink = createHttpLink({
        uri: 'http://localhost:5283/graphql', // Replace with your HTTP endpoint
    });

    const wsLink = new GraphQLWsLink(createClient({
        url: 'ws://localhost:5283/graphql', // Replace with your WebSocket endpoint
        connectionParams: () => {
            return {
                //Optional connection params
            };
        },
    }));

    const cache = new InMemoryCache();

    const apolloClient = new ApolloClient({
        link: wsLink, //Or httpLink if you don't use websockets
        cache,
    });

    provideApolloClient(apolloClient);
});